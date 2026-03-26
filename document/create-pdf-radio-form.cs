using System;
using System.IO;
using System.Runtime.InteropServices;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPath = "radio_form.pdf";

        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // ----- Question 1 -----
            TextFragment question1 = new TextFragment("1. What is the capital of France?");
            question1.Position = new Position(50, 750);
            page.Paragraphs.Add(question1);

            // Create a radio button group for Question 1
            RadioButtonField radioGroup1 = new RadioButtonField(page)
            {
                PartialName = "Q1"
            };

            // Option: Paris
            RadioButtonOptionField optParis = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 720, 70, 740))
            {
                OptionName = "Paris"
            };
            radioGroup1.Add(optParis);

            // Option: Berlin
            RadioButtonOptionField optBerlin = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(150, 720, 170, 740))
            {
                OptionName = "Berlin"
            };
            radioGroup1.Add(optBerlin);

            // Option: Madrid
            RadioButtonOptionField optMadrid = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(250, 720, 270, 740))
            {
                OptionName = "Madrid"
            };
            radioGroup1.Add(optMadrid);

            // Add the radio button field to the document's form collection
            doc.Form.Add(radioGroup1);

            // Labels for the options
            TextFragment labelA = new TextFragment("Paris");
            labelA.Position = new Position(80, 730);
            page.Paragraphs.Add(labelA);

            TextFragment labelB = new TextFragment("Berlin");
            labelB.Position = new Position(180, 730);
            page.Paragraphs.Add(labelB);

            TextFragment labelC = new TextFragment("Madrid");
            labelC.Position = new Position(280, 730);
            page.Paragraphs.Add(labelC);

            // ----- Question 2 -----
            TextFragment question2 = new TextFragment("2. Select your preferred color:");
            question2.Position = new Position(50, 660);
            page.Paragraphs.Add(question2);

            // Create a radio button group for Question 2
            RadioButtonField radioGroup2 = new RadioButtonField(page)
            {
                PartialName = "Q2"
            };

            // Option: Red
            RadioButtonOptionField optRed = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(50, 630, 70, 650))
            {
                OptionName = "Red"
            };
            radioGroup2.Add(optRed);

            // Option: Green
            RadioButtonOptionField optGreen = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(150, 630, 170, 650))
            {
                OptionName = "Green"
            };
            radioGroup2.Add(optGreen);

            // Option: Blue
            RadioButtonOptionField optBlue = new RadioButtonOptionField(page, new Aspose.Pdf.Rectangle(250, 630, 270, 650))
            {
                OptionName = "Blue"
            };
            radioGroup2.Add(optBlue);

            // Add the second radio button field to the document's form collection
            doc.Form.Add(radioGroup2);

            // Labels for the color options
            TextFragment labelRed = new TextFragment("Red");
            labelRed.Position = new Position(80, 640);
            page.Paragraphs.Add(labelRed);

            TextFragment labelGreen = new TextFragment("Green");
            labelGreen.Position = new Position(180, 640);
            page.Paragraphs.Add(labelGreen);

            TextFragment labelBlue = new TextFragment("Blue");
            labelBlue.Position = new Position(280, 640);
            page.Paragraphs.Add(labelBlue);

            // ----- Save the PDF -----
            // Guard the Save call on non‑Windows platforms where libgdiplus may be missing.
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                doc.Save(outputPath);
                Console.WriteLine($"PDF saved to '{outputPath}'.");
            }
            else
            {
                try
                {
                    doc.Save(outputPath);
                    Console.WriteLine($"PDF saved to '{outputPath}' (non‑Windows platform)." );
                }
                catch (TypeInitializationException ex) when (ContainsDllNotFound(ex))
                {
                    Console.WriteLine("Warning: GDI+ (libgdiplus) is not available on this platform. The PDF was not saved.");
                }
            }
        }
    }

    // Helper to detect a nested DllNotFoundException (e.g., missing libgdiplus)
    private static bool ContainsDllNotFound(Exception? ex)
    {
        while (ex != null)
        {
            if (ex is DllNotFoundException)
                return true;
            ex = ex.InnerException;
        }
        return false;
    }
}
