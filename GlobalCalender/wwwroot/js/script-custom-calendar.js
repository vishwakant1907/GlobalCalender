var routeURL = location.protocol + "//" + location.host;
$(document).ready(function () {
    $("#trainingDate").kendoDateTimePicker({
        value: new Date(),
        dateInput: false
    });

    //var value = kendoDateTimePicker.value();
    //kendo.toString(value, "dd/MM/YYYY hh:mm tt")

    InitializeCalendar();
});
var calendar;
function InitializeCalendar() {
    try {

        var calendarEl = document.getElementById('calendar');
        if (calendarEl != null) {
            calendar = new FullCalendar.Calendar(calendarEl, {
                initialView: 'dayGridMonth',
                headerToolbar: {
                    left: 'prev,next,today',
                    center: 'title',
                    right: 'dayGridMonth,timeGridWeek,timeGridDay'
                },
                selectable: true,
                editable: false,
                select: function (event) {
                    onShowModal(event, null);
                },
                eventDisplay: 'block',
               events: function (fetchInfo, successCallback, failureCallback) {
                    $.ajax({
                        url: routeURL + '/api/Training/GetCalendarData?trainerId=' + $("#TrainerId").val(),
                        type: 'GET',
                        dataType: 'JSON',
                        success: function (response) {
                            debugger;
                            var events = [];
                            if (response.status === 1) {
                                $.each(response.dataenum, function (i, data) {
                                    debugger;
                                    events.push({
                                        courseName: data.courseName,
                                        preRequisite: data.preRequisite,
                                        start: data.startDate,
                                        end: data.endDate,
                                        backgroundColor: data.isTrainerApproved ? "#28a745" : "#dc3545",
                                        borderColor: "#162466",
                                        textColor: "white",
                                        id: data.id
                                    });
                                })
                            }
                            successCallback(events);
                        },
                        error: function (xhr) {
                            $.notify("Error", "error");
                        }
                    });
                },
                eventClick: function (info) {
                    getEventDetailsByEventId(info.event);
                }
            });
            calendar.render();
        }

    }
    catch (e) {
        alert(e);
    }

}

function onShowModal(obj, isEventDetail) {
    if (isEventDetail != null) {
        $("#coursename").val(obj.courseName);
        $("#prerequisite").val(obj.preRequisite);
        $("#trainingDate").val(obj.startDate);
        $("#duration").val(obj.duration);
        $("#TrainerId").val(obj.trainerId);
        $("#userId").val(obj.userId);
        $("#id").val(obj.id);
        $("#lblUserName").html(obj.userName);
        $("#lblTrainerName").html(obj.trainerName);
        if (obj.isTrainerApproved) {
            $("#lblStatus").html('Approved');
            $("#btnConfirm").addClass("d-none");
            $("#btnSubmit").addClass("d-none");
        }
        else {
            $("#lblStatus").html('Pending');
            $("#btnConfirm").removeClass("d-none");
            $("#btnSubmit").removeClass("d-none");
        }

        $("#btnDelete").removeClass("d-none");
    }
    else {
        $("#trainingDate").val(obj.startStr + " " + new moment().format("hh:mm A"));
        $("#id").val(0);
        $("#btnDelete").addClass("d-none");
        $("#btnSubmit").removeClass("d-none");
    }
    $("#trainingInput").modal("show");
}

function onCloseModal() {
    $("#TrainingForm")[0].reset();
    $("#id").val(0);
    $("#coursename").val('');
    $("#prerequisite").val('');

    $("#trainingInput").modal("hide");
}

function onSubmitForm() {
    if (checkValidation()) {
        var requestData = {
            Id: parseInt($("#id").val()),
            CourseName: $("#coursename").val(),
            PreRequisite: $("#prerequisite").val(),
            StartDate: $("#trainingDate").val(),
            Duration: $("#duration").val(),
            TrainerId: $("#TrainerId").val(),
           /* UserId: $("#userId").val()*/
        };
        $.ajax({
            url: routeURL + '/api/Training/SaveCalendarData',
            type: 'POST',
            data: JSON.stringify(requestData),
            contentType: 'application/json',
            success: function (response) {
                if (response.status === 1 || response.status === 2) {
                    calendar.refetchEvents();
                    $.notify(response.message, "success");
                    onCloseModal();
                }
                else {
                    $.notify(response.message, "error");
                }
            },
            error: function (xhr) {
                $.notify("Error", "error");
            }
        });
    }
}

function checkValidation() {
    var isValid = true;
    if ($("#coursename").val() === undefined || $("#coursename").val() === "") {
        isValid = false;
        $("#coursename").addClass('error');
    }
    else {
        $("#coursename").removeClass('error');
    }

    if ($("#trainingDate").val() === undefined || $("#trainingDate").val() === "") {
        isValid = false;
        $("#trainingDate").addClass('error');
    }
    else {
        $("#trainingDate").removeClass('error');
    }

    return isValid;
}

function getEventDetailsByEventId(info) {
    $.ajax({
        url: routeURL + '/api/Training/GetCalendarDataById/' + info.id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {

            if (response.status === 1 && response.dataenum !== undefined) {
                onShowModal(response.dataenum, true);
            }
            successCallback(events);
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}

function onTrainerChange() {
    calendar.refetchEvents();
}

function onDeleteTraining(){
    var id = parseInt($("#id").val());
    $.ajax({
        url: routeURL + '/api/Training/DeleteAppoinment/' + id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {

            if (response.status === 1) {
                $.notify(response.message, "success");
                calendar.refetchEvents();
                onCloseModal();
            }
            else {

                $.notify(response.message, "error");
            }
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });
}

function onConfirm() {
    var id = parseInt($("#id").val());
    $.ajax({
        url: routeURL + '/api/Training/ConfirmEvent/' + id,
        type: 'GET',
        dataType: 'JSON',
        success: function (response) {

            if (response.status === 1) {
                $.notify(response.message, "success");
                calendar.refetchEvents();
                onCloseModal();
            }
            else {

                $.notify(response.message, "error");
            }
        },
        error: function (xhr) {
            $.notify("Error", "error");
        }
    });

}