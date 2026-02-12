using System;
using System.IO;
using System.Text;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // HTML content that will be converted to PDF
        string htmlContent = @"
            <html>
                <head>
                    <title>Sample HTML to PDF</title>
                </head>
                <body>
                    <h1 style='color:blue;'>Hello Aspose.Pdf</h1>
                    <p>This PDF document was created programmatically from HTML.</p>
                </body>
            </html>";

        // Convert the HTML string to a UTF‑8 memory stream
        using (MemoryStream htmlStream = new MemoryStream(Encoding.UTF8.GetBytes(htmlContent)))
        {
            // Load options for HTML import
            HtmlLoadOptions loadOptions = new HtmlLoadOptions();

            // Load the HTML into an Aspose.Pdf Document
            Document pdfDocument = new Document(htmlStream, loadOptions);

            // Save the resulting PDF to a file
            pdfDocument.Save("output.pdf");
        }

        Console.WriteLine("PDF created successfully: output.pdf");
    }
}