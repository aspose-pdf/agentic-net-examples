using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Text;

public class Program
{
    public static async Task Main(string[] args)
    {
        string xlsxPath = "source.xlsx";
        string pdfOutputPath = "filled.pdf";

        // Verify that the XLSX source file exists before attempting to read it
        if (!File.Exists(xlsxPath))
        {
            Console.Error.WriteLine($"Error: The source file '{xlsxPath}' was not found.");
            return;
        }

        // Asynchronously read the entire XLSX file into a byte array using the built‑in helper
        byte[] xlsxData = await File.ReadAllBytesAsync(xlsxPath);

        // Create a new PDF document and add a page
        using (Document pdfDocument = new Document())
        {
            Page page = pdfDocument.Pages.Add();

            // Add a simple text fragment that shows a preview of the XLSX data
            string preview = Convert.ToBase64String(xlsxData, 0, Math.Min(20, xlsxData.Length)) + "...";
            TextFragment textFragment = new TextFragment("XLSX preview: " + preview);
            page.Paragraphs.Add(textFragment);

            // Save the PDF to a memory stream
            using (MemoryStream pdfMemoryStream = new MemoryStream())
            {
                pdfDocument.Save(pdfMemoryStream);

                // Asynchronously write the PDF stream to a file
                pdfMemoryStream.Position = 0;
                using (FileStream pdfFileStream = new FileStream(pdfOutputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, useAsync: true))
                {
                    await pdfMemoryStream.CopyToAsync(pdfFileStream);
                }
            }
        }

        Console.WriteLine("PDF created and saved to " + pdfOutputPath);
    }
}
