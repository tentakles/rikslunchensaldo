function viewBalance() {

   // alert("HEJ");
    var cardnumber = document.getElementById('cardnumber').value;
    document.location = cardnumber;
}

window.onload = addEnterSubmit;

function addEnterSubmit() {
    document.getElementById("cardnumber").addEventListener("keydown", function(e) {

        if (!e) {
            var e = window.event;
        }
      //  e.preventDefault(); // sometimes useful

        // Enter is pressed
        if (e.keyCode == 13) {
            viewBalance();
        }
    }, false);
}