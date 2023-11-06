using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FileManager.Model;

[Route("link")]
[ApiController]
public class FileManagerController : ControllerBase
{
    [HttpPost]
    public IActionResult CheckFile([FromBody] FileManagerModel request)
    {
        try
        {    //Se o json estiver em branco ou ser um valor nulo passa por esse caminho
            if (request == null || string.IsNullOrWhiteSpace(request.Url))
            {
                return BadRequest(new { message = "Caminho informado em branco ou nulo!" });  
            }

            string path = request.Url;

            if (System.IO.File.Exists(path)|| Directory.Exists(path))
            {
                try
                {
                    // Se o caminho é um arquivo, ele é aberto aqui.
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true
                    });
                    return Ok(new { message = "path aberto com sucesso." });
                }
                catch (SystemException ex)
                {
                    var msg = "Erro ao abrir arquivo. "+ex.Message;
                    System.Console.WriteLine(msg);
                    return BadRequest(new {message = msg});
                }
            }
            else
            {
                // Se o caminho não existe, retorna um não encontrado.
                return NotFound();
            }
        }
        catch (Exception ex)
        {
            // Lidar com exceções, se necessário.
            return StatusCode(500, "Teste de msg de retorno! "+ex.Message);
        }
    }}