// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function pageForward(pageNum) {
    document.getElementById("pageNumber").value = pageNum + 1
    document.getElementById("crash-filter-form").submit()
}

function pageBackwards(pageNum) {
    document.getElementById("pageNumber").value = pageNum - 1
    document.getElementById("crash-filter-form").submit()
}

function revealRow(id) {
    document.getElementById("hiddenRow" + id).hidden= false
}

function hideRow(id) {
    document.getElementById("hiddenRow" + id).hidden = true
}