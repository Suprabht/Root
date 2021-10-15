using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DailyVisitors.DAL.Models;
using System.IO;
using DailyVisitors.WebApi.Services;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace DailyVisitors.WebApi.Controllers
{
    [Route("api/[controller]")]
   // [Authorize]
    [ApiController]
    public class VisitorDetailsController : ControllerBase
    {
        private readonly VisitorsBookContext _context;
        private readonly IReportService _reportService;

        public VisitorDetailsController(VisitorsBookContext context, IReportService reportService)
        {
            _context = context;
            _reportService = reportService;
        }

        //[Authorize]
        // GET: api/VisitorDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VisitorDetails>>> GetVisitorDetails()
        {
            return await _context.VisitorDetails.Where(x => x.IsDeleted == false).ToListAsync();
        }

		//GET: api/VisitorDetail/5
        [HttpGet("{id}")]
		public async Task<ActionResult<VisitorDetails>> GetVisitorDetails(long id)
		{
			var visitorDetails = await _context.VisitorDetails.FindAsync(id);

			if (visitorDetails == null)
			{
				return NotFound();
			}

			return visitorDetails;
		}

		// PUT: api/VisitorDetail/5
		[HttpPut("{id}")]
        public async Task<IActionResult> PutVisitorDetails(long id, VisitorDetails visitorDetails)
        {
            if (id != visitorDetails.VisitorId)
            {
                return  BadRequest();
            }

            _context.Entry(visitorDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!VisitorDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok("Exception" + exception.Message);
                }
            }

            return NoContent();
        }

        // POST: api/VisitorDetail
        [HttpPost]
        public async Task<ActionResult<VisitorDetails>> PostVisitorDetails(VisitorDetails visitorDetails)
        {
            try
            {
                visitorDetails.Signature = SaveImage(visitorDetails.Signature);
                visitorDetails.Picture = SaveImage(visitorDetails.Picture);
                visitorDetails.LoginDateTime = DateTime.UtcNow;
                visitorDetails.LogoutDateTime = null;
                visitorDetails.IsDeleted = false;
                _context.VisitorDetails.Add(visitorDetails);
                await _context.SaveChangesAsync();
                //var visitorDetailsTosend = await _context.VisitorDetails.FindAsync(visitorDetails.VisitorId);
                return CreatedAtAction("GetVisitorDetails", new { id = visitorDetails.VisitorId }, visitorDetails);
            }
            catch(Exception ex)
            {
                return CreatedAtAction("Error", ex.Message.ToString());
            }
           
        }

        // DELETE: api/VisitorDetail/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IEnumerable<VisitorDetails>>> DeleteVisitorDetails(long id)
        {
            var visitorDetails = _context.VisitorDetails.FirstOrDefault(visitor => visitor.VisitorId == id);
            if (id != visitorDetails.VisitorId)
            {
                return BadRequest();
            }

            _context.Entry(visitorDetails).State = EntityState.Modified;
            visitorDetails.IsDeleted = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await _context.VisitorDetails.Where(x=>x.IsDeleted==false).ToListAsync();
        }

        //api/VisitorDetails/logoutById?id=1
        [HttpGet("logoutById")]
        public async Task<ActionResult<IEnumerable<VisitorDetails>>> Get(int id)
        {
            var visitorDetails = _context.VisitorDetails.FirstOrDefault(visitor => visitor.VisitorId == id);
            if (id != visitorDetails.VisitorId)
            {
                return BadRequest();
            }

            _context.Entry(visitorDetails).State = EntityState.Modified;
            visitorDetails.LogoutDateTime = DateTime.UtcNow;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitorDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return await _context.VisitorDetails.Where(x=>x.IsDeleted==false).ToListAsync();
        }

        //api/VisitorDetails/emailDetails?id=1
        [HttpGet("emailDetails")]
        public IActionResult EmailDetails(long id, string emailId)
        {
            
            try
            {
                var visitorDetails = _context.VisitorDetails.FirstOrDefault(visitor => visitor.VisitorId == id);
                var url = Request.Scheme + System.Uri.SchemeDelimiter + Request.Host + "/";
                var pictureUrl = url + visitorDetails.Picture;
                var signatureUrl = url + visitorDetails.Signature;
                /*
                var html = string.Format("@<table cellpadding=0 >" +
                    "<tr><td><table cellpadding=0 style='font-size:11px; width:100%'><tr><td style='height:40px;'>Id:- </td><td>#{0}</td></tr> <tr><td style='height:40px;'>Name:- </td><td>{1}</td></tr><tr><td style='height:40px;'>Email:- </td><td>{2}</td></tr><tr><td style='height:40px;'>Mobile No:- </td><td>{3}</td></tr><tr><td style='height:40px;'>Address:- </td><td>{4}</td></tr><tr><td style='height:40px;'>Company Name:- </td><td>{5}</td></tr><tr><td style='height:40px;'>Person Visiting in RWS:- </td><td>{6}</td></tr><tr><td style='height:40px;'>Login Date:- </td><td>{7}</td></tr><tr><td style='height:40px;'>Log out Date:- </td><td>{8}</td></tr></table></td><td style='padding-right: 10px;'><img style=‘width:330px; height:230px’ src=‘{9}’ /><br/><img style=‘width:330px; height:230px’ src=‘{10}’ /></td></tr></table>");
                */
                var html = string.Format(@"<table cellpadding=0 ><tr><td>
                    <table cellpadding=0 style='font-size:11px; width:100%'>
	                    <tr>
	                    <td style='height:40px;'>
		                    Id:- </td>
                                    <td>#{0}</td>
                                  </tr>
                                  <tr>
                                      <td style='height:40px;'>Name:- </td>
                                      <td>{1}</td>
                                  </tr>
                                  <tr>
                                    <td style='height:40px;'>Email:- </td>
                                    <td>{2}</td>
                                  </tr>
                                  <tr>
                                    <td style='height:40px;'>Mobile No:- </td>
                                    <td>{3}</td>
                                  </tr>
                                  <tr>
                                    <td style='height:40px;'>Address:- </td>
                                    <td>{4}</td>
                                  </tr><tr>
                                      <td style='height:40px;'>Company Name:- </td>
                                      <td>{5}</td>
                                  </tr>
                                  <tr>
                                      <td style='height:40px;'>Person Visiting in RWS:- </td>
                                      <td>{6}</td>
                                  </tr>
                                  <tr>
                                      <td style='height:40px;'>Login Date:- </td>
                                      <td>{7}</td>
                                  </tr>
                                  <tr>
                                    <td style='height:40px;'>Log out Date:- </td>
                                    <td>{8}</td>
                                  </tr>
                                </table>
                              </td>
                              <td style='padding-right: 10px;'>
                                <img style='width:330px; height:230px' src='{9}' /><br/>
		                        <img style='width:330px; height:230px' src='{10}' />
                              </td>
                            </tr>
                          </table>",
                          visitorDetails.VisitorId,
                          visitorDetails.VisitorName,
                          visitorDetails.Email,
                          visitorDetails.MobileNumber,
                          visitorDetails.Adress,
                          visitorDetails.Company,
                          visitorDetails.VisitorName,
                          visitorDetails.LoginDateTime.ToString(),
                          visitorDetails.LogoutDateTime,
                          pictureUrl,
                          signatureUrl);
                var result = (new EmailService()).Send(emailId, "Details of Visitor #" + visitorDetails.VisitorId, html);
                if (result.Contains("Success"))
                    return Ok(new { Message = "Success: Email send successfully." });
                else
                    return Ok(new { Message = "Error: Email server not responding." });
            }
            catch (DbUpdateConcurrencyException exception)
            {
                if (!VisitorDetailsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { Message = "Error:" + exception.Message });
                }
            }

        }

        //api/VisitorDetails/visitorDetailsPDF?id=1
        [HttpGet("visitorDetailsPDF")]
        public IActionResult visitorDetailsPDF(long id)
        {
            var visitorDetails = _context.VisitorDetails.FirstOrDefault(visitor => visitor.VisitorId == id);
            var url = Request.Scheme + System.Uri.SchemeDelimiter + Request.Host + "/";
            var pictureUrl = url + visitorDetails.Picture;
            var signatureUrl = url + visitorDetails.Signature;
            
            var html = string.Format(@"<!DOCTYPE html>
                   <html lang=""en"">
                   <head>
                    <meta charset = ""UTF-8"">
                       <title>Visitor Details</title>
                   </head>
                  <body style=""font-family:'Ariel'; font-size:70px;"">
                    <table>
                        <tr>
                        <td>
                        <table>
	                    <tr>
	                    <td style=""font-weight:bold"">
		                    Id:- </td>
                                    <td>#{0}</td>
                                  </tr>
                                  <tr>
                                      <td style=""font-weight:bold"">Name:- </td>
                                      <td>{1}</td>
                                  </tr>
                                  <tr>
                                    <td style=""font-weight:bold"">Email:- </td>
                                    <td>{2}</td>
                                  </tr>
                                  <tr>
                                    <td style=""font-weight:bold"">Mobile No:- </td>
                                    <td>{3}</td>
                                  </tr>
                                  <tr>
                                    <td style=""font-weight:bold"">Address:- </td>
                                    <td>{4}</td>
                                  </tr><tr>
                                      <td style=""font-weight:bold"">Company Name:- </td>
                                      <td>{5}</td>
                                  </tr>
                                  </table>
                              </td>
                              <td>
                                <table>
                                    <tr>
                                      <td style=""font-weight:bold"">Person Visiting in RWS:- </td>
                                      <td>{6}</td>
                                  </tr>
                                  <tr>
                                      <td style=""font-weight:bold"">Login Date:- </td>
                                      <td>{7}</td>
                                  </tr>
                                  <tr>
                                    <td style=""font-weight:bold"">Log out Date:- </td>
                                    <td>{8}</td>
                                  </tr>
                                </table>
                              </td>
                            </tr>
                            <tr>
                                <td colspan=""2"">{9} {10}</td>
                            </tr>
                          </table></body>
                  </html>",
                          visitorDetails.VisitorId,
                          visitorDetails.VisitorName,
                          visitorDetails.Email,
                          visitorDetails.MobileNumber,
                          visitorDetails.Adress,
                          visitorDetails.Company,
                          visitorDetails.VisitorName,
                          visitorDetails.LoginDateTime.ToString(),
                          visitorDetails.LogoutDateTime,
                          pictureUrl,
                          signatureUrl);
            var pdfFile = _reportService.GeneratePdfReport(html);
            return File(pdfFile,
            "application/octet-stream", "visitorDetailsPDF.pdf");
        }

        //api/VisitorDetails/badgePDF?id=1
        [HttpGet("badgePDF")]
        public IActionResult badgePDF(long id)
        {
            var visitorDetails = _context.VisitorDetails.FirstOrDefault(visitor => visitor.VisitorId == id);
            var html = string.Format(@"<!DOCTYPE html>
                   <html lang=""en"">
                   <head>
                    <meta charset = ""UTF-8"">
                       <title>Visitor Details</title>
                   </head>
                  <body style=""font-family:'Ariel'; font-size:60px;"">
            <table cellpadding=0 style="" border:#000 solid 2px;"">
                <tr>
                  <td style=""padding:30px; border-right:#000 solid 2px; width:1400px; vertical-align:top;"">
 
                     <table cellpadding = 0>
    
                            <tr>
    
                                <td style="""" > &nbsp;</td>
         
                                     <td style=""text-align:right;height:300px"" ><strong> Badge &nbsp; &nbsp;</strong ></td >
                   
                                           </tr>
                   
                                           <tr>
                   
                                               <td style=""height:200px""><strong> Name:- </strong></td>
                          
                                                      <td>{0}</td>
                               
                                                       </tr>
                               
                                                       <tr>
                                                        <td style=""height:200px"">
                                                                <strong> Company Name: -</strong>
                                                        </td>
                                                        <td>{1}</td>
                                                        </tr><tr>
                                            
                                                                <td style=""height:200px"" ><strong> Person Visiting in RWS: - </strong></td>
                                                    
                                                                                <td>{2}</td>
                                                         
                                                                                 </tr>
                                                         
                                                                                 <tr>
                                                         
                                                                                     <td style=""height:200px"" ><strong> Date:- </strong></td>
                                                                
                                                                                            <td>{3}</td>
                                                                     
                                                                                             </tr>
                                                                     
                                                                                         </table>
                                                                     
                                                                                       </td>
                                                                     
                                                                                       <td style="" padding: 30px;width:1400px;"" >
                                                                      
                                                                                          <strong> THROUGHOUT YOUR VISIT YOUR PERSONAL SAFETY IS OUR CONCERN<br/> -WE THERFORE REQUEST THAT YOU ABIDE BY THE FOLLOWING:</strong><br/><br/>
                                                                          
                                                                                              <strong> HEALTH & SAFETY -</strong> All visitors are subject to the Health &Safety at Work Act 1974, Management of Health & Safety at work Regulations 1999 and Company Regulations whilst on the premises.<br/>
                                                                              
                                                                                                  <strong> SMOKING -</strong> Please observe the No Smoking policy.<br/>
                                                                                   
                                                                                                       <strong> FIRE / EMERGENCY -</strong> In the event of an emergency, all visitors must leave the premises immediatly via the nearest safe exit and report to the designated assembly point.Do not re-enter the premises until you are advised it is safe to do so.<br/>
                    <strong>CONTRACTORS -</strong> Must ensure that any necessary Permit to work documenttion is completed before commencing work.
                    <strong>ACCIDENTS/INCIDENTS -</strong> All accidents, injures and near misses must be immediately reported.
                 </td>
                </tr>
            </table></body></html>",
                         visitorDetails.VisitorName,
                          visitorDetails.Company,
                          visitorDetails.VisitorName,
                          visitorDetails.LoginDateTime.ToString());
            var pdfFile = _reportService.GeneratePdfReport(html);

            return File(pdfFile,
            "application/octet-stream", "badgePDF.pdf");
        }

        //api/VisitorDetails/downloadExcel?id=1
        [HttpGet("downloadExcel")]
        public IActionResult downloadExcel()
        {
            var url = Request.Scheme + System.Uri.SchemeDelimiter + Request.Host + "/";
            //List<VisitorDetails> obj = new List<VisitorDetails>();
            var obj = _context.VisitorDetails.Where(x => x.IsDeleted == false).ToList<VisitorDetails>();
            StringBuilder str = new StringBuilder();
            str.Append("<table border=`" + "1px" + "`b>");
            str.Append("<tr>");
            str.Append("<td><b><font face=Arial Narrow size=3>Id</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Name</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Email</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Mobile No</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Address</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Company</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Person to Visit</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Login Date Time</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Logout Date Time</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Photo</font></b></td>");
            str.Append("<td><b><font face=Arial Narrow size=3>Signature</font></b></td>");
            str.Append("</tr>");
            foreach (VisitorDetails visitor in obj)
            {
                str.Append("<tr>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + visitor.VisitorId.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + visitor.VisitorName.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + visitor.Email.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + visitor.MobileNumber.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + visitor.Adress.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + visitor.Company.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + visitor.VisitorName.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + visitor.LoginDateTime.ToString("dd/MM/yyyy HH:mm:ss") + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + visitor.LogoutDateTime?.ToString("dd/MM/yyyy HH:mm:ss") + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + url + visitor.Picture.ToString() + "</font></td>");
                str.Append("<td><font face=Arial Narrow size=" + "14px" + ">" + url + visitor.Signature.ToString() + "</font></td>");

                str.Append("</tr>");
            }
            str.Append("</table>");
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers.Add("content-disposition", "attachment; filename=Report" + DateTime.Now.Year.ToString() + ".xls");
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            return File(temp, "application/vnd.ms-excel");
        }

        //api/VisitorDetails/emailReport?visitorIds=1&visitorIds=23&visitorIds=89
        [HttpGet("emailReport")]
        public IActionResult emailReport([FromQuery] List<long> visitorIds, string emailId)
        {
            try
            {
                var url = Request.Scheme + System.Uri.SchemeDelimiter + Request.Host + "/";
                var obj = _context.VisitorDetails.Where(x => x.IsDeleted == false).Where(x => visitorIds.Contains(x.VisitorId)).ToList<VisitorDetails>();
                StringBuilder str = new StringBuilder();
                str.Append("<!DOCTYPE html><html lang=\"en\"><body style=\"font-family:'Ariel'; font-size:11px;\">");
                str.Append("<table style='width:100%'>");
                str.Append("<tr>");
                str.Append("<td style='padding:5px; border-bottom:1px solid #000; background-color:#DAF7A6'><b>Id</b></td>");
                str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Name</b></td>");
                str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Email</b></td>");
                str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Mobile No</b></td>");
                str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Company</b></td>");
                str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Person to Visit</b></td>");
                str.Append("</tr>");
                foreach (VisitorDetails visitor in obj)
                {
                    str.Append("<tr>");
                    str.Append("<td style='padding:5px'>#" + visitor.VisitorId.ToString() + "</td>");
                    str.Append("<td>" + visitor.VisitorName.ToString() + "</td>");
                    str.Append("<td>" + visitor.Email.ToString() + "</td>");
                    str.Append("<td>" + visitor.MobileNumber.ToString() + "</td>");
                    str.Append("<td>" + visitor.Company.ToString() + "</td>");
                    str.Append("<td>" + visitor.VisitorName.ToString() + "</td>");
                    str.Append("</tr><tr>");
                    str.Append("<td colspan=\"6\" style='padding:5px'><table tyle='width:100%'><tr><td><img style='width:330px; height:230px' src='" + url + visitor.Picture.ToString() + "'/></td><td><img style='width:330px; height:230px' src='" + url + visitor.Signature.ToString() + "'/></td></tr></table></td>");
                    str.Append("</tr><tr>");
                    str.Append("<td colspan=\"6\" style='padding:5px'><table tyle='width:100%'><tr><td style='width:50%'><strong>Address:-<br/></strong>" + visitor.Adress.ToString() + "</td><td><strong>Login Time:- </strong>" + visitor.LoginDateTime.ToString("dd/MM/yyyy HH:mm:ss") + "<br/><strong>Logout Time:- " + visitor.LogoutDateTime?.ToString("dd/MM/yyyy HH:mm:ss") + "</strong></td></tr></table></td>");
                    str.Append("</tr><tr>");
                    str.Append("<td colspan=\"6\"style='padding:5px; border-top:1px solid #000;'>&nbsp;</td>");
                    str.Append("</tr>");
                }
                str.Append("</table></body>");
                var result = (new EmailService()).Send(emailId, "Details of Report ", str.ToString());
                if (result.Contains("Success"))
                    return Ok(new { Message = "Success: Email send successfully" });
                else
                    return Ok(new { Message = "Error: Email server not responding." });
            }
            catch (Exception exception )
            {
                return Ok(new { Message = "Error: " + exception.Message });

            }

        }

        //api/VisitorDetails/downloadPDF?visitorIds=1&visitorIds=23&visitorIds=89
        [HttpGet("downloadPDF")]
        public IActionResult downloadPDF([FromQuery] List<long> visitorIds)
        {
            var url = Request.Scheme + System.Uri.SchemeDelimiter + Request.Host + "/";
            var obj = _context.VisitorDetails.Where(x => x.IsDeleted == false).Where(x => visitorIds.Contains(x.VisitorId)).ToList<VisitorDetails>();
            StringBuilder str = new StringBuilder();
            str.Append("<!DOCTYPE html><html lang=\"en\"><head><meta charset = \"UTF-8\"><title>Visitor Details</title></head><body style=\"font-family:'Ariel'; font-size:60px;\">");
            str.Append("<table style='width:2900px'>");
            str.Append("<tr>");
            str.Append("<td style='padding:20px; border-bottom:1px solid #000; background-color:#DAF7A6'><b>Id</b></td>");
            str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Name</b></td>");
            str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Email</b></td>");
            str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Mobile No</b></td>");
            str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Company</b></td>");
            str.Append("<td style='border-bottom:1px solid #000; background-color:#DAF7A6'><b>Person to Visit</b></td>");
            str.Append("</tr>");
            foreach (VisitorDetails visitor in obj)
            {
                str.Append("<tr>");
                str.Append("<td style='padding:20px'>#" + visitor.VisitorId.ToString() + "</td>");
                str.Append("<td>" + visitor.VisitorName.ToString() + "</td>");
                str.Append("<td>" + visitor.Email.ToString() + "</td>");
                str.Append("<td>" + visitor.MobileNumber.ToString() + "</td>");
                str.Append("<td>" + visitor.Company.ToString() + "</td>");
                str.Append("<td>" + visitor.VisitorName.ToString() + "</td>");
                str.Append("</tr><tr>");
                str.Append("<td colspan=\"6\" style='padding:20px'><strong>Picture:- </strong>" + url + visitor.Picture.ToString() + "</td>");
                str.Append("</tr><tr>");
                str.Append("<td colspan=\"6\" style='padding:20px'><strong>Signature:- </strong>" + url + visitor.Signature.ToString() + "</td>");
                str.Append("</tr><tr>");
                str.Append("<td colspan=\"6\" style='padding:20px'><strong>Address:- </strong>" + visitor. Adress.ToString() + "</td>");
                str.Append("</tr><tr>");
                str.Append("<td colspan=\"6\" style='padding:20px'><strong>Login date and time:- </strong>" + visitor.LoginDateTime.ToString("dd/MM/yyyy HH:mm:ss") + "</td>");
                str.Append("</tr><tr>");
                str.Append("<td colspan=\"6\" style='padding:20px'><strong>Logout date and time:- </strong>" + visitor.LogoutDateTime?.ToString("dd/MM/yyyy HH:mm:ss") + "</td>");
                str.Append("</tr><tr>");
                str.Append("<td colspan=\"6\"style='padding:20px; border-top:1px solid #000;'>&nbsp;</td>");
                str.Append("</tr>");
            }
            str.Append("</table></body>");
            var pdfFile = _reportService.GeneratePdfReport(str.ToString());

            return File(pdfFile,
            "application/octet-stream", "badgePDF.pdf");
        }

        //api/VisitorDetails/downloadCSV?visitorIds=1&visitorIds=23&visitorIds=89
        [HttpGet("downloadCSV")]
        public IActionResult downloadCSV([FromQuery] List<long> visitorIds)
        {
            var url = Request.Scheme + System.Uri.SchemeDelimiter + Request.Host + "/";
            var obj = _context.VisitorDetails.Where(x => x.IsDeleted == false).Where(x=>visitorIds.Contains(x.VisitorId)).ToList<VisitorDetails>();
            StringBuilder str = new StringBuilder();
            
            str.Append("Id,");
            str.Append("Name,");
            str.Append("Email,");
            str.Append("Mobile No,");
            str.Append("Address,");
            str.Append("Company,");
            str.Append("Person to Visit,");
            str.Append("Login Date Time,");
            str.Append("Logout Date Time,");
            str.Append("Photo,");
            str.Append("Signature,");
            str.Append("\r\n");
            foreach (VisitorDetails visitor in obj)
            {
                str.Append(visitor.VisitorId.ToString() + ",");
                str.Append(visitor.VisitorName.ToString() + ",");
                str.Append(visitor.Email.ToString() + ",");
                str.Append(visitor.MobileNumber.ToString() + ",");
                str.Append(visitor.Adress.Replace(",","\"").ToString() + ",");
                str.Append(visitor.Company.ToString() + ",");
                str.Append(visitor.VisitorName.ToString() + ",");
                str.Append(visitor.LoginDateTime.ToString("dd/MM/yyyy HH:mm:ss") + ",");
                str.Append(visitor.LogoutDateTime?.ToString("dd/MM/yyyy HH:mm:ss") + ",");
                str.Append(url + visitor.Picture.ToString() + ",");
                str.Append(url + visitor.Signature.ToString() + ",");
                str.Append("\r\n");
            }
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.Headers.Add("content-disposition", "attachment; filename=Report" + DateTime.Now.Year.ToString() + ".csv");
            byte[] temp = System.Text.Encoding.UTF8.GetBytes(str.ToString());
            return File(temp, "application/vnd.ms-excel");
        }

        private bool VisitorDetailsExists(long id)
        {
            return _context.VisitorDetails.Any(e => e.VisitorId == id);
        }

        public string SaveImage(string base64image)
        {
            var bytes = Convert.FromBase64String(base64image);      
            var folderName = Path.Combine("StaticFiles", "Images");
            var filedir = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (!Directory.Exists(filedir))
            {
                Directory.CreateDirectory(filedir);
            }
            var timestamp = DateTime.Now.ToFileTime();
            var fileName = timestamp.ToString() + ".png";
            var file = Path.Combine(filedir, fileName);
            if (bytes.Length > 0)
            {
                using (var stream = new FileStream(file, FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
            }
            return Path.Combine(folderName, fileName);
        }
    }
}
