const newBody = document.querySelector('body');

newBody.addEventListener('click', e => {
    if (e.target.tagName === 'SELECT') {
        var id = e.target.value
        console.log(Number(id))
    }
})
