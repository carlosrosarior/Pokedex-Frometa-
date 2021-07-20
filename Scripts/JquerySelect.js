const select = document.querySelector('#select');
const opciones = document.querySelector('#opciones');
const contenidoSelect = document.querySelector("#select .contenido-select");
const hiddenInput = document.querySelector("#PictureId");
document.querySelectorAll('#opciones > .opcion').forEach((opciones)=>{
    opciones.addEventListener('click',(e) =>{
        e.preventDefault();
        contenidoSelect.innerHTML = e.currentTarget.innerHTML;
        select.classList.toggle('active');
        document.querySelector('#opciones').classList.toggle('active');
        hiddenInput.value = e.currentTarget.querySelector('.titulo').innerText;
    });
});
select.addEventListener('click',()=>{
    select.classList.toggle('active');
    opciones.classList.toggle('active');
});