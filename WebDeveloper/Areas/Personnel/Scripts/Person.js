
var rowsByPage = 15;
var totalPage = 1;
var currentPage = 1;

var baseUrl = '';

$(function () {
    baseUrl = document.getElementById('baseUrl').value;
    goToPage(1);
    getTotalPage();
       
});

function goToPage(page) {
      
    var finalUrl = baseUrl + "/List?page=" + page + "&size=" + rowsByPage;
    $.get(finalUrl, function (data) {
        $('#personContent').html(data);
        currentPage = page;
    });

}


function getTotalPage() {

      
    var finalUrl = baseUrl  + "/PageTotal?rows=" + rowsByPage;
    $.get(finalUrl, function (data) {
        totalPage = data;
        setPaginator();
    });
}

function setPaginator(){
    
    $(".paginator").bootpag({
        total: totalPage,
        page: 1,
        maxVisible: 5,
        leaps: true,
        firstLastUse: true,
        first: '←',
        last: '→',
        wrapClass: 'pagination',
        activeClass: 'active',
        disabledClass: 'disabled',
        nextClass: 'next',
        prevClass: 'prev',
        lastClass: 'last',
        firstClass: 'first'
    }).on("page", function (event, num) {
        goToPage(num);
    });
}



function changeSize() {

    rowsByPage = document.getElementById('rowsByPage').value;

    if(rowsByPage){
        getTotalPage();
        goToPage(1);
    }
}

function updatePage(){

    goToPage(currentPage);

}
