const poinClass = "point";
const poinClassPrev = "pointPrev";
const poinClassNext = "pointNext";

function initNavBar() {
    const navbar = document.getElementById("nav-bar");
    const navButtons = document.getElementsByClassName("nav-button");
    for (let i = 0; i < navButtons.length; i++) {
        navButtons[i].addEventListener('mouseenter', () => navButtonEnter(navButtons, navButtons[i], i, navButtons.length));
    }
}

function navButtonEnter(allElements, element, i, l) {
    removeAllPointers(allElements);
    element.classList.add(poinClass);
    if (i < l - 1) {
        allElements[i + 1].classList.add(poinClassNext);
    }
    if (i > 0) {
        allElements[i - 1].classList.add(poinClassPrev);
    }
}

function removeAllPointers(allElements) {
    for (let i = 0; i < allElements.length; i++) {
        if (allElements[i].classList.contains(poinClass)) {
            allElements[i].classList.remove(poinClass);
        }
        if (allElements[i].classList.contains(poinClassPrev)) {
            allElements[i].classList.remove(poinClassPrev);
        }
        if (allElements[i].classList.contains(poinClassNext)) {
            allElements[i].classList.remove(poinClassNext);
        }
    }
}

document.addEventListener('DOMContentLoaded', initNavBar);