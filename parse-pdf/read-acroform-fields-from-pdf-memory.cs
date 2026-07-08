using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Try to open an existing PDF file; if it does not exist, create a sample PDF in memory.
        const string pdfPath = "input.pdf";
        Document sourceDoc;

        if (File.Exists(pdfPath))
        {
            // Load the existing file via a read‑only FileStream.
            using (FileStream fs = File.OpenRead(pdfPath))
            {
                sourceDoc = new Document(fs);
            }
        }
        else
        {
            // Create a minimal PDF that contains an AcroForm field.
            sourceDoc = CreateSamplePdfWithForm();
        }

        // Save the document to a MemoryStream so that the subsequent read does not touch the disk.
        using (MemoryStream ms = new MemoryStream())
        {
            sourceDoc.Save(ms);
            ms.Position = 0; // Reset the stream before reading.

            // Load the PDF from the memory stream and enumerate its form fields.
            using (Document doc = new Document(ms))
            {
                if (doc.Form != null && doc.Form.Fields != null && doc.Form.Fields.Any())
                {
                    Console.WriteLine("AcroForm fields found:");
                    foreach (Field field in doc.Form.Fields)
                    {
                        string value = field.Value?.ToString() ?? "<empty>";
                        Console.WriteLine($"- {field.FullName}: {value}");
                    }
                }
                else
                {
                    Console.WriteLine("No AcroForm fields present in the document.");
                }
            }
        }
    }

    // Helper that builds a PDF containing a single text box field.
    private static Document CreateSamplePdfWithForm()
    {
        Document doc = new Document();
        Page page = doc.Pages.Add();

        // Define a rectangle for the text box field (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y).
        var fieldRect = new Rectangle(100, 600, 300, 620);
        TextBoxField txtField = new TextBoxField(page, fieldRect)
        {
            PartialName = "SampleTextBox",
            Value = "Hello World"
        };
        doc.Form.Add(txtField);

        // Optional visual label.
        page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Sample Form PDF"));

        return doc;
    }
}