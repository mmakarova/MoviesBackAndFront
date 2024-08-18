$(function () {
    const baseUrl = "http://localhost:5184/";
    const getAllMovies = baseUrl + "api/movies";
    const pageSize = 4;

    const searchTextBox = $('#filter').dxTextBox({
        placeholder: 'Введите название фильма',
        showClearButton: true
    }).dxTextBox('instance');

    $('#searchButton').dxButton({
        text: 'Поиск',
        onClick() {
            CreateQueryAndFetchData();
        }
    });

    const list = $("#listMovies").dxList({
        itemTemplate: function (data) {
            const result = $('<div>').addClass('card');
            $('<img>').attr('alt', data.title).attr('src', data.posterUrl).appendTo(result);
            $('<div>').text(data.title).appendTo(result);
            $('<span>').text('Год выпуска: ' + data.yearOfRelease).appendTo(result);
            return result;
        }
    }).dxList('instance');

    const pagerSelector = $("#pager");
    const pager = pagerSelector.pagination({
        cssStyle: 'light-theme',
        itemsOnPage: pageSize,
        useAnchors: false,
        prevText: 'Пред.',
        nextText: 'След.',
        onPageClick: function (pageNumber) {
            CreateQueryAndFetchData(pageNumber);
        }
    });

    CreateQueryAndFetchData();

    
    function CreateQueryAndFetchData(pageNumber = 1) {
        const params = new URLSearchParams();
        const titleSearchString = searchTextBox.option('value');
        params.append('pagesize', pageSize);
        params.append('page', pageNumber);
        if (titleSearchString) params.append('title', titleSearchString);
        const getAllMoviesWithOptions = getAllMovies + '?' + params.toString();
        fetch(getAllMoviesWithOptions)
            .then(response => response.json())
            .then(data => {
                list.option('dataSource', data.items);
                pager.pagination('updateItems', data.total);
            });
    }

});