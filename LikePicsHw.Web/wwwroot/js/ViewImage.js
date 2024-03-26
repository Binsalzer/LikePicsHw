$(() => {
    const id = $('#image-id').val()

    $('#like-button').on('click', function () {
        $.post('/home/like', { id }, function () {
        })
    })

    setInterval(function () {
        $.get('/home/GetLikeCount', { id }, function ( {likesCount}) {
            $('#likes-count').text(likesCount)
        })
    }, 1000)




})