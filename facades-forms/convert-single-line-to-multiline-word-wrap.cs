using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdf);

        // Retrieve the "Address" text box field using the Fields collection
        TextBoxField addressField = pdfDocument.Form?.Fields?
            .FirstOrDefault(f => f.Name == "Address") as TextBoxField;

        if (addressField == null)
        {
            Console.Error.WriteLine("Form field 'Address' not found or is not a text box.");
            return;
        }

        // Enable multiline (auto‑wrap) for the field
        addressField.Multiline = true;

        // Adjust the field height manually – Aspose.Pdf does not expose an AutoSize property for height.
        // Increase the rectangle height by a reasonable amount (e.g., 50 points). Adjust as needed.
        var rect = addressField.Rect;
        addressField.Rect = new Rectangle(rect.LLX, rect.LLY, rect.URX, rect.LLY + 50);

        // Save the modified PDF
        pdfDocument.Save(outputPdf);

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}
