// quando entrar na página
// requisicao HTTP GET
// JSON 
// console.table(arrayUsuarios)

$.ajax({
    //url: 'https://localhost:44300/api/v1/usuarios' (HTTPS com SSL - porta 44300),
    url: 'http://localhost:64393/api/v1/usuarios' /* HTTP - sem segurança - porta 64393 */ ,
    type: 'GET',
    dataType: 'json',
    contentType: 'application/json',
    success: function (usuariosCadastrados) {
        // 200 Ok
        // 202 Created
        // 204 No Content
        //console.table(usuariosCadastrados);

        // fazendo uma iteração com o foreach
        usuariosCadastrados.forEach(usuario => {
            console.log(usuario)
           // $('#mensagem').html('<h2>Usuarios Cadastrados!</h2>');

            let linha = `
            <tr>
                <td> ${usuario.id} </td> 
                <td> ${usuario.nome} </td>
                <td> ${usuario.idade} </td>
                <td> ${usuario.email} </td>
            </tr>
         `;
            $('#tbody').append(linha)
            // aqui você precisa colocar um código pra fazer aparecer CADA UM dos usuarios
            // na tabela
        });
    },
    error: function (errorData) {
        // 400 - Bad Request
        // 500 - Internal Server Error
        // 404 - Not Found
        console.log("Deu errado", errorData);
        $('#mensagem').html('<h2>Ocorreu um erro na sua requisição</h2>');
    }
});
const botaoEntrar = document.getElementById('botao-entrar');
botaoEntrar.addEventListener('click', aoClicarNoBotao);

function aoClicarNoBotao() {

    console.log("Evento click reconhecido:");

    // let nome = document.getElementById('nome').value;
    // nome = $('#nome').val(); // é a mesma coisa da linha de cima, porém mais simplificado
    // let idade = document.getElementById('idade').value;
    // let email = document.getElementById('email').value;
    // let senha = document.getElementById('senha').value;

    // $('aqui dentro precisa ter um selector')
    // $('#blabla') estou buscando um elemento com o ID blabla, pois é isso que o # significa
    // $('.blabla') estou buscando um elemento com a CLASS blabla, pois é isso que o '.' significa
    let nome = $('#nome').val();
    let idade = $('#idade').val();
    let email = $('#email').val();
    let senha = $('#senha').val();

    // objeto JavaScript
    let usuarioParaCriacao = {
        nome: nome,
        idade: idade,
        email: email,
        senha: senha
    }

    // transforma objeto em JavaScript para objeto em JSON.
    let usuarioParaCriacaoEmJson = JSON.stringify(usuarioParaCriacao);

    // Requisição HTTP
    $.ajax({
        //url: 'https://localhost:44300/api/v1/usuarios' (HTTPS com SSL - porta 44300),
        url: 'http://localhost:64393/api/v1/usuarios' /* HTTP - sem segurança - porta 64393 */ ,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: usuarioParaCriacaoEmJson,
        success: function (usuarioCriado) {
            // 200 Ok
            // 202 Created
            // 204 No Content
            console.log("Deu certo", usuarioCriado);

            // através do Jquery ele pega o elemento no HTML com o ID Mensagem
            // ou seja: <div id+="mensagem">
            // com o .html() ele injeta o código '<h2>....</h2>' dentro do HTML
            // ou seja, no final, a página htm l vai ficar assim:
            /*
                <div id="mensagem"> 
                    <h2>Sua requisição deu certo!</h2>
                </div>
            */
            $('#mensagem').html('<h2>Sua requisição deu certo!</h2>');

            // string interpolation q é usar crase e colocar variaveis no meio
            // pra colocar variaveis no meio usamos ${nome da variavel}
            let linhasHtml = `
                <tr>
                    <td> ${usuarioCriado.id} </td>
                    <td> ${usuarioCriado.nome} </td>
                    <td> ${usuarioCriado.idade} </td>
                    <td> ${usuarioCriado.email} </td>
                </tr>
            `;

            // .html() ele limpa tudo oq já tinha e injeta tudo novo
            // o .append() ele  adiciona um conteúdo a algo existente
            // ou seja. com o .append() a gente não perde o q ja tinha na div
            $('#tbody').append(linhasHtml)

        },
        error: function (errorData) {
            // 400 - Bad Request
            // 500 - Internal Server Error
            // 404 - Not Found
            console.log("Deu errado", errorData);
            $('#mensagem').html('<h2>Ocorreu um erro na sua requisição</h2>');
        }
    });
}