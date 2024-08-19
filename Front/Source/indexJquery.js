$(function () {
    const baseUrl = "http://localhost:5184/";
    const getAllMovies = baseUrl + "api/movies";
    const getAllGenres = baseUrl + "api/genres";
    const pageSize = 4;
    let genres =[];

    GetGenres();

    const filterTitle = $('#filterTitle').dxTextBox({
        placeholder: 'Введите название фильма',
        showClearButton: true
    }).dxTextBox('instance');
    const filterGenre = $('#filterGenre').dxSelectBox({        
        valueExpr: "id",
        displayExpr: "title",
        placeholder: 'Выберите жанр',
        showClearButton: true  
    }).dxSelectBox('instance');
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
            //add tags
            $('<br>').appendTo(result);
            data.genres.forEach(element => {
                let el = genres.find(x=>x.id == element).title;
                $('<span>').addClass('tag').text(el).appendTo(result);                
            });            
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

    function GetGenres() {
        fetch(getAllGenres)
            .then(response => response.json())
            .then(data => {
                genres = data.items;
                filterGenre.option('dataSource', genres);
            });
    }
    
    function CreateQueryAndFetchData(pageNumber = 1) {
        const params = new URLSearchParams();
        const titleSearchString = filterTitle.option('value');
        const genreIdSearchString = filterGenre.option('value');
        params.append('pagesize', pageSize);
        params.append('page', pageNumber);
        if (titleSearchString) params.append('title', titleSearchString);        
        if (genreIdSearchString) params.append('genreid', genreIdSearchString);
        const getAllMoviesWithOptions = getAllMovies + '?' + params.toString();
        fetch(getAllMoviesWithOptions)
            .then(response => response.json())
            .then(data => {
                list.option('dataSource', data.items);
                pager.pagination('updateItems', data.total);
            });
    }

});