namespace ShoppingManagement.Extensions.Services;

using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

public static class SwaggerServiceExtension
{
    public static void AddSwaggerExtension(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Record Label - Shopping API",
                    Description = "Our API uses a REST based design, leverages the JSON data format, and relies upon HTTPS for transport. We respond with meaningful HTTP response codes and if an error occurs, we include error details in the response body. API Documentation is at carbonkitchen.com/dev/docs",
                    Contact = new OpenApiContact
                    {
                        Name = "Record Label Shopping Management",
                        Email = "devsupport@recordlabel.com",
                            Url = new Uri("https://www.recordlabel.com"),
                    },
                });

            config.IncludeXmlComments(string.Format(@$"{AppDomain.CurrentDomain.BaseDirectory}{Path.DirectorySeparatorChar}ShoppingManagement.WebApi.xml"));
        });
    }
}