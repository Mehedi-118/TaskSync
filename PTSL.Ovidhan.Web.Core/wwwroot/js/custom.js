"use strict";

function _getBeneficiaryFilterData(filterData, successFunction, errorFunction) {
    $.ajax({
        type: "POST",
        url: "/BeneficiaryProfile/GetFilterData",
        data: filterData,
        success: successFunction,
        error: errorFunction,
    });
}

//function resetFromData(selector) {
//    document.querySelector(selector).reset();
//}

function printReport() {
    window.print();
}

function resetFromData(selector) {
    window.location.reload();
}

function addRequiredFieldInPage() {
    const classes = ["#ForestCircleId",
        "#ForestDivisionId",
        "#ForestRangeId",
        "#ForestBeatId",
        "#ForestFcvVcfId",
        "#DistrictId",
        "#DivisionId",
        "#UpazillaId",
        "#UnionId",
        "#NgoId"];

    classes.map(function (c) {
        const label = $(c).parent().find("label");

        if (label.length > 0) {
            label.append(' <span style="color:red;">*</span>');
        }
    });
}

function formateDate(date) {
    return new Intl.DateTimeFormat('en-US',
    {
        hour12: true,
        weekday: 'short',
        year: 'numeric',
        month: 'short',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
        second: 'numeric',
    }
    ).format(new Date(date));
}

function getMonthName(monthNumber) {
    const months = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    return months[monthNumber - 1];
}

// Custom Validators
const _isInteger = num => /^-?[0-9]+$/.test(num + '');
const _isEmail = (email) => {
    return /^\w+@@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/.test(email);
}
const _isEnglish = (str) => {
    return /^[\x20-\x7E]*$/.test(str);
}

$.validator.addMethod(
    '_mustBeInteger',
    function (value, element, requiredValue) {
        return _isInteger(value);
    },
    'Must be a valid number.'
);
$.validator.addMethod(
    '_isEmail',
    function (value) {
        return isEmail(value);
    },
    'Invalid Email'
);
$.validator.addMethod(
    '_isEnglish',
    function (value) {
        return _isEnglish(value);
    },
    'Please enter English characters'
);
