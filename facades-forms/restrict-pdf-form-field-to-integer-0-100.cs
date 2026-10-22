using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // PDF containing the "Score" field
        const string outputPath = "output.pdf"; // PDF after applying validation

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Document class
        Document pdfDocument = new Document(inputPath);

        // Retrieve the field named "Score" and cast to TextBoxField
        TextBoxField scoreField = pdfDocument.Form["Score"] as TextBoxField;
        if (scoreField == null)
        {
            Console.Error.WriteLine("Field \"Score\" not found or is not a text box.");
            return;
        }

        // JavaScript validation to allow only integer values between 0 and 100
        string js = @"if (event.value < 0 || event.value > 100) {
    app.alert('Please enter a score between 0 and 100.');
    event.rc = false;
}";
        scoreField.Actions.OnValidate = new JavascriptAction(js);

        // Save the modified PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Validation applied. Output saved to '{outputPath}'.");
    }
}