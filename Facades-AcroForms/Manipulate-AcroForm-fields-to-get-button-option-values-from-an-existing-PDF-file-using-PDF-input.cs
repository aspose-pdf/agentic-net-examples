using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source PDF containing AcroForm fields
        const string inputPdfPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Error: PDF file not found at '{inputPdfPath}'.");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPdfPath);

        // Access the AcroForm object
        Form form = pdfDocument.Form;

        // Iterate through all form fields and output values of button-type fields
        foreach (WidgetAnnotation field in form)
        {
            // ButtonField covers push buttons, check boxes, and radio buttons
            if (field is ButtonField button)
            {
                // FullName provides the fully qualified field name
                // Value returns the current option/value of the button field
                Console.WriteLine($"Button field '{button.FullName}' has value: {button.Value}");
            }
        }

        // Optionally save the document (no modifications made) using the standard save rule
        const string outputPdfPath = "output.pdf";
        pdfDocument.Save(outputPdfPath);
    }
}