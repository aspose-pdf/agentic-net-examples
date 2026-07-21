using System;
using System.Collections;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "PdfForm.pdf";
        const string fieldName = "Color";

        // ---------------------------------------------------------------------
        // 1. Create a PDF with a radio‑button group named "Color" and a few options.
        //    This guarantees the file exists in the sandbox before we open it with
        //    the Form facade.
        // ---------------------------------------------------------------------
        using (Document doc = new Document())
        {
            // Add a single page – the radio button will be placed on this page.
            Page page = doc.Pages.Add();

            // Define the rectangle where the radio button field will be placed.
            // (left, bottom, right, top) – values are in points.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create the radio button field. Use the constructor that accepts only a Page.
            RadioButtonField radio = new RadioButtonField(page)
            {
                Rect = rect,
                PartialName = fieldName // internal name used by the Form facade
            };

            // Add options (display text, export value).
            radio.AddOption("Red", "Red");
            radio.AddOption("Green", "Green");
            radio.AddOption("Blue", "Blue");

            // Add the field to the document's form collection.
            doc.Form.Add(radio);

            // Save the seed PDF so the Form facade can read it.
            doc.Save(pdfPath);
        }

        // ---------------------------------------------------------------------
        // 2. Open the PDF with the Form facade and retrieve the option values.
        // ---------------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(pdfPath))
        {
            // GetButtonOptionValues returns a Dictionary<string, string>
            // where the key is the option's display name and the value is the export value.
            Dictionary<string, string> optionDict = form.GetButtonOptionValues(fieldName);

            // Transfer the dictionary entries into a Hashtable as required by the task.
            Hashtable optionTable = new Hashtable();
            foreach (KeyValuePair<string, string> kvp in optionDict)
            {
                optionTable.Add(kvp.Key, kvp.Value);
            }

            // Example output.
            foreach (DictionaryEntry entry in optionTable)
            {
                Console.WriteLine($"{entry.Key} = {entry.Value}");
            }
        }
    }
}
