using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "SessionFilled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document.
        Document pdfDocument = new Document(inputPath);

        // Generate a GUID for the hidden SessionID field.
        string sessionId = Guid.NewGuid().ToString();

        // Find the field named "SessionID" and set its value.
        // The field may be referenced by Name or FullName, so we check both.
        var sessionField = pdfDocument.Form?.Fields?
            .FirstOrDefault(f => string.Equals(f.Name, "SessionID", StringComparison.OrdinalIgnoreCase) ||
                                 string.Equals(f.FullName, "SessionID", StringComparison.OrdinalIgnoreCase));

        if (sessionField != null)
        {
            sessionField.Value = sessionId;
        }
        else
        {
            Console.Error.WriteLine("Field 'SessionID' not found in the PDF form.");
        }

        // Save the modified PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Hidden SessionID field filled. Output saved to '{outputPath}'.");
    }
}
