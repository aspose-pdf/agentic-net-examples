using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text; // for FormattedText

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "rotated_annotation.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Initialize the facade for stamping
        PdfFileStamp fileStamp = new PdfFileStamp();
        fileStamp.BindPdf(inputPdf); // load source PDF

        // Create a text stamp (acts as a text annotation)
        Aspose.Pdf.Facades.Stamp textStamp = new Aspose.Pdf.Facades.Stamp();

        // FormattedText constructor requires System.Drawing.Color for the text color
        FormattedText ft = new FormattedText(
            "Rotated Note",                 // text
            System.Drawing.Color.Black,     // text color
            "Helvetica",                    // font name
            EncodingType.Winansi,           // encoding
            false,                          // embed font?
            12);                            // font size

        // Bind the formatted text to the stamp
        textStamp.BindLogo(ft);

        // Rotate the stamp by 90 degrees
        textStamp.Rotation = 90f;

        // Add the stamp (annotation) to the document
        fileStamp.AddStamp(textStamp);

        // Save the modified PDF
        fileStamp.Save(outputPdf);

        // Release resources held by the facade
        fileStamp.Close();

        Console.WriteLine($"Rotated text annotation saved to '{outputPdf}'.");
    }
}