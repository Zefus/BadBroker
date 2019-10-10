function validate(startDate, endDate, score) {

    var validation = {
        success: true,
        message: ""
    };

    if (startDate === "") {
        validation.success = false;
        validation.message += "Start Date cannot be empty<br>";
    }

    if (endDate === "") {
        validation.success = false;
        validation.message += "End Date cannot be empty<br>";
    }

    if (score <= 0) {
        validation.success = false;
        validation.message += "Account balance should not be 0<br>";
    }

    return validation;
}

function error(message) {
    $('#errorMessage').empty();
    $('#errorMessage').append(message);
    $('#modal').modal('show');
}

