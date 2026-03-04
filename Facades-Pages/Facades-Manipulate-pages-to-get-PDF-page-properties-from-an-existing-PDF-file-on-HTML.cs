using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "page_properties.html";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Use PdfFileInfo facade to access page properties
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPdf))
        {
            int pageCount = pdfInfo.NumberOfPages; // 1‑based page count

            // Create an HTML file with a table of page properties
            using (StreamWriter writer = new StreamWriter(outputHtml))
            {
                writer.WriteLine("<!DOCTYPE html>");
                writer.WriteLine("<html><head><meta charset=\"UTF-8\"><title>PDF Page Properties</title></head><body>");
                writer.WriteLine("<h1>PDF Page Properties</h1>");
                writer.WriteLine("<table border=\"1\" cellpadding=\"5\" cellspacing=\"0\">");
                writer.WriteLine("<tr><th>Page</th><th>Width (pt)</th><th>Height (pt)</th><th>Rotation (°)</th></tr>");

                for (int i = 1; i <= pageCount; i++) // Aspose.Pdf uses 1‑based indexing
                {
                    double width = pdfInfo.GetPageWidth(i);
                    double height = pdfInfo.GetPageHeight(i);
                    int rotation = pdfInfo.GetPageRotation(i);

                    writer.WriteLine($"<tr><td>{i}</td><td>{width}</td><td>{height}</td><td>{rotation}</td></tr>");
                }

                writer.WriteLine("</table>");
                writer.WriteLine("</body></html>");
            }

            Console.WriteLine($"Page properties saved to '{outputHtml}'.");
        }
    }
}