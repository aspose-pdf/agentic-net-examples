using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "Header";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // -----------------------------------------------------------------
            // 1. Set visual attributes for the field using FormEditor + Facade
            // -----------------------------------------------------------------
            FormEditor formEditor = new FormEditor(doc);
            formEditor.Facade = new FormFieldFacade();

            // Center the text inside the field
            formEditor.Facade.Alignment = FormFieldFacade.AlignCenter;

            // Background color (System.Drawing.Color) – fully qualified to avoid ambiguity
            formEditor.Facade.BackgroundColor = System.Drawing.Color.LightGray;

            // Apply the visual settings to the specific field named "Header"
            formEditor.DecorateField(fieldName);

            // -----------------------------------------------------------------
            // 2. Add a background image to the same field area (using a stamp)
            // -----------------------------------------------------------------
            // Retrieve the field to obtain its rectangle
            var field = doc.Form[fieldName];
            if (field != null)
            {
                const string backgroundImagePath = "header_background.jpg";
                if (File.Exists(backgroundImagePath))
                {
                    // Use the Facade version of Stamp and fully qualify it
                    Aspose.Pdf.Facades.Stamp bgStamp = new Aspose.Pdf.Facades.Stamp();
                    bgStamp.BindImage(backgroundImagePath);
                    // Position the stamp to match the field rectangle (cast to float as required)
                    bgStamp.SetOrigin((float)field.Rect.LLX, (float)field.Rect.LLY);
                    // Make the stamp appear behind the existing content
                    bgStamp.IsBackground = true;

                    // Add the stamp to the document
                    using (PdfFileStamp fileStamp = new PdfFileStamp(doc))
                    {
                        fileStamp.AddStamp(bgStamp);
                    }
                }
                else
                {
                    Console.Error.WriteLine($"Background image not found: {backgroundImagePath}");
                }
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }
    }
}
