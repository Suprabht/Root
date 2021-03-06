﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Dal.Models.Identity;
using Microsoft.Extensions.Options;

namespace WebApp.TagHelper
{
    public class RolesTagHelper : Microsoft.AspNetCore.Razor.TagHelpers.TagHelper
    {
        private BridgeToCareContext _context;
        public RolesTagHelper( BridgeToCareContext context)
        {
            _context = context;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var jsonString = "<div id=\"jstree\"><ul>";
            foreach (var role in _context.AspNetRoles.ToList())
            {
                // jsonString += "<li onclick=\"javascript:roles.addUser(\'" + role.Id + "\');\">";
                jsonString += "<li class='roleli' roleId ='" + role.Id + "'>";
                jsonString +=  (role.Name.Equals("Hr"))? "Human Resources" : role.Name ;
                jsonString += "<ul>";
                foreach (var userRoles in _context.AspNetUserRoles.ToList().Where(n => n.RoleId == role.Id).ToList())
                {

                    if (userRoles.UserId != null)
                    {
                        var user = _context.AspNetUsers.ToList().Where(u => u.Id == userRoles.UserId);
                        if (user != null)
                        {
                            jsonString += "<li class='userli' userId='" + userRoles.User.Id + "' onclick=\"javascript:roles.callUser(\'" + userRoles.User.Id + "\');\">";
                            jsonString += userRoles.User.Email;
                            jsonString += "</li>";
                        }
                    }
                }
                jsonString += "</ul>";
                jsonString += "</li>";
            }
            jsonString += "</ul></div>";
            output.Content.SetHtmlContent(jsonString);
        }

    }
}
