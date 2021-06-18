// quando entrar na página
// requisicao HTTP GET
// JSON 
// console.table(arrayUsuarios)

$.ajax({
    //url: 'https://localhost:44300/api/v1/empresas' (HTTPS com SSL - porta 44300),
    url: 'http://localhost:64393/api/v1/empresas' /* HTTP - sem segurança - porta 64393 */ ,
    type: 'GET',
    dataType: 'json',
    contentType: 'application/json',
    success: function (empresasCadastradas) {
        // 200 Ok
        // 202 Created
        // 204 No Content
        //console.table(usuariosCadastrados);

        // fazendo uma iteração com o foreach
        empresasCadastradas.forEach(empresa => {
            console.log(empresa)
           // $('#mensagem').html('<h2>Usuarios Cadastrados!</h2>');

            let linha = `
            <tr>
                <td> ${empresa.id} </td> 
                <td> ${empresa.nomeDaEmpresa} </td>
                <td> ${empresa.enderecoDaEmpresa} </td>
                <td> ${empresa.telefone} </td>
                <td> ${empresa.cnpj} </td> 
                <td> ${empresa.email} </td>
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

    // let nomeDaEmpresa = document.getElementById('nomeDaEmpresa').value;
    // nomeDaEmpresa = $('#nomeDaEmpresa').val(); // é a mesma coisa da linha de cima, porém mais simplificado
    // let enderecoDaEmpresa = document.getElementById('enderecoDaEmpresa').value;
    // let telefone = document.getElementById('telefone').value;
    // let cnpj = document.getElementById('cnpj').value;
    // let email = document.getElementById('email').value;
    // let senha = document.getElementById('senha').value;

    // $('aqui dentro precisa ter um selector')
    // $('#blabla') estou buscando um elemento com o ID blabla, pois é isso que o # significa
    // $('.blabla') estou buscando um elemento com a CLASS blabla, pois é isso que o '.' significa
    let nomeDaEmpresa = $('#nomeDaEmpresa').val();
    let enderecoDaEmpresa = $('#enderecoDaEmpresa').val();
    let telefone = $('#telefone').val();
    let cnpj = $('#cnpj').val();
    let email = $('#email').val();
    let senha = $('#senha').val();

    // objeto JavaScript
    let empresaParaCriacao = {
        nomeDaEmpresa: nomeDaEmpresa,
        enderecoDaEmpresa: enderecoDaEmpresa,
        telefone: telefone,
        cnpj: cnpj,
        email: email,
        senha: senha
    }

    // transforma objeto em JavaScript para objeto em JSON.
    let empresaParaCriacaoEmJson = JSON.stringify(empresaParaCriacao);

    // Requisição HTTP
    $.ajax({
        //url: 'https://localhost:44300/api/v1/empresas' (HTTPS com SSL - porta 44300),
        url: 'http://localhost:64393/api/v1/empresas' /* HTTP - sem segurança - porta 64393 */ ,
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        data: empresaParaCriacaoEmJson,
        success: function (empresaCriada) {
            // 200 Ok
            // 202 Created
            // 204 No Content
            console.log("Deu certo", empresaCriada);

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
                    <td> ${empresaCriada.id} </td>
                    <td> ${empresaCriada.nomeDaEmpresa} </td>
                    <td> ${empresaCriada.enderecoDaEmpresa} </td>
                    <td> ${empresaCriada.telefone} </td>
                    <td> ${empresaCriada.cnpj} </td>
                    <td> ${empresaCriada.email} </td>
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