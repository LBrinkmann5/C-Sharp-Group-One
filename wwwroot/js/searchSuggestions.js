'use strict'; 
function setupAirportAutocomplete(inputId, suggestionsId, displayId, codeId) {
    var $input = $('#' + inputId);
    var $suggestions = $('#' + suggestionsId);
    var $display = $('#' + displayId);
    var $code = $('#' + codeId);

    $input.on('input', function () {
        var term = $input.val();
        $code.val('');
        if (term.length < 3) {
            $suggestions.hide();
            return;
        }
        $.get('/Home/SearchAirports', { term: term }, function (data) {
            $suggestions.empty();
            if (data && data.length) {
                data.forEach(function (airport) {
                    $suggestions.append(
                        $('<a href="#" class="list-group-item list-group-item-action"></a>')
                            .text(airport.name + ' (' + airport.code + ') - ' + airport.city + ', ' + airport.state)
                            .data('airport', airport)
                    );
                });
                $suggestions.show();
            } else {
                $suggestions.hide();
            }
        });
    });

    $suggestions.on('click', 'a', function (e) {
        e.preventDefault();
        var airport = $(this).data('airport');
        $input.val(airport.code);
        $code.val(airport.code);
        $suggestions.hide();
        if ($display)
        {
            $display.text(airport.name);
        }
    });

    $(document).on('click', function (e) {
        if (!$(e.target).closest($input).length && !$(e.target).closest($suggestions).length) {
            $suggestions.hide();
        }
    });
}

$(function () {
    setupAirportAutocomplete('airport-search-depart', 'airport-suggestions-depart', 'airport-depart-name', 'airport-depart-code' );
    setupAirportAutocomplete('airport-search-arrival', 'airport-suggestions-arrival', 'airport-arrival-name', 'airport-arrival-code');
});

$('form').on('submit', function () {
    if (!$('#airport-depart-code').val())
    {
        alert('Please select a departure airport.');
        e.preventDefault();
    }
    else if (!$('#airport-arrival-code').val()) {
        alert('Please select an arrival airport.');
        e.preventDefault();
    }


});