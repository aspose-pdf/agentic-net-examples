using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "rotated_stamp.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the source PDF document.
        using (Document srcDoc = new Document(inputPdf))
        {
            // Initialize the PdfFileStamp facade and bind it to the loaded document.
            using (PdfFileStamp fileStamp = new PdfFileStamp())
            {
                fileStamp.BindPdf(srcDoc);

                // Create a text stamp.
                // FormattedText constructor requires System.Drawing.Color for the text color.
                FormattedText ft = new FormattedText(
                    "Rotated Text",                     // text
                    System.Drawing.Color.Black,         // text color
                    "Helvetica",                        // font name
                    EncodingType.Winansi,               // encoding
                    false,                              // embed font
                    24);                                // font size

                Aspose.Pdf.Facades.Stamp stamp = new Aspose.Pdf.Facades.Stamp();
                stamp.BindLogo(ft);

                // Set rotation to 90 degrees.
                stamp.Rotation = 90f;

                // Add the stamp to all pages of the document.
                fileStamp.AddStamp(stamp);

                // Save the stamped PDF.
                fileStamp.Save(outputPdf);
                fileStamp.Close();
            }
        }

        // Verify that the text is still readable by extracting it from the output PDF.
        using (Document outDoc = new Document(outputPdf))
        {
            TextAbsorber absorber = new TextAbsorber();
            outDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            Console.WriteLine("Extracted text after stamping:");
            Console.WriteLine(extractedText);
        }

        Console.WriteLine($"Aspose.Pdf.Facades.Stamp with 90° rotation applied and saved to '{outputPdf}'.");
    }
}