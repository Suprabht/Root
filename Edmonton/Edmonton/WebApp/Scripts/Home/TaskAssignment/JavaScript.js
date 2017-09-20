function fechaReg(el) {
    jQuery(el).datetimepicker({
        dateFormat: 'yy-mm-dd',
        timeFormat: 'hh:mm:ss',
        changeYear: true,
        changeMonth: true,
        numberOfMonths: 1,
        timeOnlyTitle: 'Seleccione Horario',
        timeText: 'Hora seleccionada',
        hourText: 'Hora',
        minuteText: 'Minuto',
        secondText: 'Segundo',
        millisecText: 'Milisegundo',
        currentText: 'Ahora',
        closeText: 'Listo',
        ampm: false
    });
}

 jQuery("#correspondence").jqGrid({
        url: '../ajax/selectdata.php',
        editurl: '../ajax/editdata.php',
        mtype: 'post',
        datatype: "json",
        colNames: ['id', 'name', 'total_dept', 'Nombre', 'Fecha y Hora', 'Fecha', 'Hora', 'Consecutivo', 'Tipo de requerimiento', 'Tipo de medio', 'Tipo de Remitente', 'Detalle', 'Observaciones', 'Añadir propiedad', 'Añadir Area'],
        colModel: [
            {name: 'id', index: 'id', width: 20, align: "right", hidden: true,editable: false,  editrules: {required: false}},
            {name: 'name', index: 'name', width: 50, editable: true,hidden:true, sorttype: 'text', editrules: {required: true, edithidden: true}},
            {name: 'id_corres_property', index: 'id_corres_property', width: 20, align: "right", hidden: true},
            {name: 'total_dept', index: 'total_dept', width: 20, align: "right", hidden: true},
            {name: 'date_time', index: 'date_time', width: 20, align: "right", hidden: true, editable: true, editoptions: {dataInit: fechaReg, readonly: 'readonly'}, editrules: {required: true, edithidden: true}, search: false},
            {name: 'fecha', index: 'fecha', width: 50, editable: false, editrules: {required: true}, search: false},
            {name: 'hora', index: 'hora', width: 50, editable: false, editrules: {required: true}, search: false},
            {name: 'consecutivo', index: 'consecutivo', width: 55, editable: true, search: false},
            {name: 'type', index: 'type', width: 60, editable: true, sorttype: 'text', editrules: {required: true}, edittype: 'select', editoptions: {value: {'': '-seleccione-', Normal: 'Normal', Queja: 'Queja', Reclamo: 'Reclamo', Juridico: 'Juridico',Derecho_de_peticion:'Derecho de peticion'}}},
            {name: 'media', index: 'media', width: 50, editable: true, sorttype: 'text', editrules: {required: true}, edittype: 'select', editoptions: {value: {'': '-seleccione-', Email: 'Email', Personal: 'Personal', Mensajero: 'Mensajero', 'Correo certificado': 'Correo certificado', Fax: 'Fax', 'Pag. web': 'Pag. web'}}},
            {name: 'sender_type', index: 'sender_type', width: '80', editable: true, editrules: {required: true}, edittype: 'select', editoptions: {value: {'': '-seleccione-', Owner: 'Propietario', Renter: 'Arrendatario', Management: 'Administración', Assignee: 'Codeudor', Garante: 'Apoderado'}},
                stype: "select", searchoptions: {value: "*:Todo;Owner:Propietario;Renter:Inquilino;Management:Administración;Assignee:Codeudor;Garante:Apoderado", defaultValue: "*"}
            },
            {name: 'details', index: 'details', width: 80, editable: true}, 
            {name: 'observations', index: 'observations', width: 80, editable: true, edittype: 'textarea', editoptions: {rows: "2", cols: "60"}},
            {name: 'boton_person', index: 'boton_property', sortable: false, width: '50', align: "center", search: false},
            {name: 'boton_office', index: 'boton_office', sortable: false, width: '50', align: "center", search: false}
        ],
        gridComplete: function ()
        {
            var ids = jQuery("#correspondence").jqGrid('getDataIDs');
            var allRowsInGrid = jQuery("#correspondence").jqGrid('getRowData');
            for (var i = 0; i < ids.length; i++)
            {
                if (allRowsInGrid[i]['id_corres_property'])
                {
                    aP = "<a class='cursorhand' onClick='setProperty(" + ids[i] + ")' style='cursor:pointer;'><img src='../../../admin/public/images/plus_black.png' width='16' height='16' border='0' title='Añadir propiedad' /></a>";
                }
                else
                {
                    aP = "<a class='cursorhand' onClick='setProperty(" + ids[i] + ")' style='cursor:pointer;'><img src='../../../admin/public/images/plus_green.png' width='16' height='16' border='0' title='Añadir propiedad' /></a>";
                }

                jQuery("#correspondence").jqGrid('setRowData', ids[i], {boton_person: aP});

                if (allRowsInGrid[i]['total_dept'] == 0)
                {
                    aO = "<a class='cursorhand' onClick='setArea(" + ids[i] + ")' style='cursor:pointer;'><img src='../../../admin/public/images/plus_green.png' width='16' height='16' border='0' title='Añadir Oficina' /></a>";
                }
                else
                {
                    aO = "<a class='cursorhand' onClick='setArea(" + ids[i] + ")' style='cursor:pointer;'><img src='../../../admin/public/images/plus_black.png' width='16' height='16' border='0' title='Añadir Oficina' /></a>";
                }
                jQuery("#correspondence").jqGrid('setRowData', ids[i], {boton_office: aO});
            }
        },
        width: "1100",
        rowNum: 25,
        rowList: [10, 20, 30, 40, 50],
        pager: '#correspondence-pager',
        sortname: 'id',
        viewrecords: true,
        gridview: true,
        reloadAfterSubmit: true,
        sortorder: "desc",
        caption: " ..:: Correspondencia ::.. ",
        search: true
    });
    jQuery('#correspondence').jqGrid('filterToolbar', {"stringResult": true, "searchOnEnter": false});
    jQuery("#correspondence").jqGrid('navGrid', '#correspondence-pager', {edit: true, add: true, del: false, search: false},
    {recreateForm: true, width: 500}, {recreateForm: true, width: 500}, {recreateForm: true}, {recreateForm: true}).navButtonAdd('#correspondence-pager', {
        caption: "",
        buttonicon: "ui-icon-disk",
        onClickButton: function () {

            var filter = {daterange: "fecha"};
            exportFilter("correspondence", 'correspondance', filter);

        },
        position: "last"
    });
