using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Step 1: Create a sample PDF with a simple text box form field.
        string pdfPath = "sample.pdf";
        using (Document pdfDocument = new Document())
        {
            Page page = pdfDocument.Pages.Add();
            // Define the rectangle for the form field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y).
            Rectangle fieldRect = new Rectangle(100, 600, 300, 620);
            TextBoxField textBox = new TextBoxField(page, fieldRect);
            textBox.PartialName = "Name";
            textBox.Value = "John Doe";
            pdfDocument.Form.Add(textBox);
            pdfDocument.Save(pdfPath);
        }

        // Step 2: Open the created PDF and export its form data as XFDF (XML) into a stream.
        using (Document pdfDocument = new Document(pdfPath))
        {
            using (MemoryStream xfdfStream = new MemoryStream())
            {
                pdfDocument.ExportAnnotationsToXfdf(xfdfStream);
                xfdfStream.Position = 0;

                // In a real web scenario the xfdfStream would be written to HttpResponse.OutputStream.
                // For this self‑contained console example we write the stream to a file to demonstrate the output.
                using (FileStream fileStream = new FileStream("formdata.xfdf", FileMode.Create, FileAccess.Write))
                {
                    xfdfStream.CopyTo(fileStream);
                }
            }
        }

        Console.WriteLine("Form data exported to formdata.xfdf");
    }
}