DataBackup = function () { };
DataBackup.prototype.init = function () {
    
    $.getJSON("/Home/Program1",
        function (data) {
            var arr = [];
            for (i = 0; i < data.rows.length; i++) {
                for (i = 0; i < data.rows.length; i++) {
                    //programs[data.rows[i].programId] = data.rows[i].programCode + " : " + data.rows[i].programName + " - " + data.rows[i].programCategoryAbbreviation + " : " + data.rows[i].programCategoryName;
                    var item = { val: data.rows[i].programId, text: data.rows[i].programCode + " : " + data.rows[i].programName + " - " + data.rows[i].programCategoryAbbreviation + " : " + data.rows[i].programCategoryName };
                    arr.push(item)
                };
            };
            var sel = $('<select id="dropdownCatagory">').appendTo('#divdropdownCatagory');
            $(arr).each(function () {
                sel.append($("<option>").attr('value', this.val).text(this.text));
            });
        });
}
DataBackup.prototype.submitExcell = function () {
    $("#downloadLink").hide();
    var personId = $("#dropdownCatagory").val();
    $.ajax({
        type: 'POST',
        url: '/Home/ExportToExcel' ,
        dataType: "json",
        data: {
            Id: personId
        },
        cache: false,
        success: function (data) {
            alert("Excel is generated click on Download link to download!");
            $("#downloadLink").show();
        },
        error: function (xhr, ajaxOptions, error) {
            alert(xhr.status);
            alert("Error: " + xhr.responseText);
        }
    });
}
var dataBackup = new DataBackup();
dataBackup.init();