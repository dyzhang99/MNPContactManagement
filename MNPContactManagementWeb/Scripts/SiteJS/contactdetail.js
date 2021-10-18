﻿$(document).ready(function () {
    var $datePicker = $('#' + "txtLastDateContacted");
    $datePicker.datepicker({
        orientation: "bottom auto",
        format: 'mm/dd/yyyy',
        endDate: '+0d',
        startDate: new Date(1753, 1 - 1, 1),
        autoclose: true,
        language: 'en'
    });
})