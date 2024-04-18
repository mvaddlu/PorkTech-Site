using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

SmtpData smtpData = JsonConvert.DeserializeObject<SmtpData>(File.ReadAllText("Private/smtp_configuration.json"))!;

Console.WriteLine(smtpData);

builder.Services.AddSingleton(new EmailSender(smtpData));


var app = builder.Build();

app.MapPost("/api/form", async (HttpContext context, [FromServices]EmailSender sender, [FromBody]BusinessForm formData) => 
{
    if(formData is { BusinessName: null } or { BusinessType: null } or { ContactEmail: null } or { ContactPhone: null})
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { msg = "Not all fields were filled" });
        return;
    }

    System.Console.WriteLine(formData);

    if(formData.BusinessName!.Length > 150)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { msg = "Business Name input is too long" });
        return;
    }

    if(formData.BusinessType!.Length > 100)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { msg = "Business Type input is too long" });
        return;
    }

    if(BusinessForm.EmailRegex().IsMatch(formData.ContactEmail!))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { msg = "Email input is not a valid email" });
        return;
    }

    if(BusinessForm.PhoneRegex().IsMatch(formData.ContactPhone!))
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { msg = "Phone input is not a valid number" });
        return;
    }

    await sender.CreateLead(formData);
    
    context.Response.StatusCode = 200;
    await context.Response.WriteAsJsonAsync(new { msg = "From is successfully submitted!" });    
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();