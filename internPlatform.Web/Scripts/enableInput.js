document.addEventListener("DOMContentLoaded", function () {
    var checkbox1 = document.querySelector('.myCheckbox1');
    var checkbox2 = document.querySelector('.myCheckbox2');
    var input = document.querySelector('.myInput');

    function changeFirstCheckbox() {
        if (this.checked === true ) {
            input.disabled = true;
            input.value = "";
        } 
    }
    function changeSecondCheckbox() {
        if (this.checked === true) {
            input.disabled = false;;
        }
    }

    checkbox1.addEventListener('change', changeFirstCheckbox);
    checkbox2.addEventListener('change', changeSecondCheckbox);

    // Initial check on page load

    if (checkbox1.checked) {
        input.disabled = true;
        input.value = "";
    } else {
        input.disabled = false;
    }

});