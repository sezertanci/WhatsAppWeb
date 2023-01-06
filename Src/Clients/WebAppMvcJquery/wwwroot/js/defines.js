var apiAppLink = "http://localhost:5263/";

function dateMomentTimeOrDate(value) {
    today = dateMoment(new Date());
    date = dateMoment(value);

    return today == date ? dateMomentHour(value) : dateMomentWithHour(value);
}

function dateMomentWithHour(value) {
    return moment(value).format('DD.MM.YYYY HH:mm');
}

function dateMomentHour(value) {
    return moment(value).format('HH:mm');
}

function dateMoment(value) {
    return moment(value).format('YYYY-MM-DD');
}

function dateFormat(value) {
    date = value.split('.');

    return new Date(date[2] + "-" + date[1] + "-" + date[0] + "T00:00:00Z");
}

function getFormattedDate(date) {
    let year = date.getFullYear();
    let month = (1 + date.getMonth()).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');
    let hour = date.getHours().toString().padStart(2, '0');
    let minute = date.getMinutes().toString().padStart(2, '0');

    return year + "." + month + "." + day + " " + hour + ":" + day;
}

function PlayNotificationSound() {
    $.playSound('/media/notification_sound.mp3');
}