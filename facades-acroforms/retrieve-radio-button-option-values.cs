using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // ---------------------------------------------------------------------
        // 1. Create a temporary PDF that contains a radio‑button group.
        // ---------------------------------------------------------------------
        string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");
        const string radioFieldName = "RadioGroup";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the rectangle where the radio button will be placed.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 150, 750);

            // RadioButtonField has a constructor that accepts only a Page.
            RadioButtonField radio = new RadioButtonField(page)
            {
                PartialName = radioFieldName,
                Rect = rect
            };

            // Add a few options (display text, export value).
            radio.AddOption("Option A", "A");
            radio.AddOption("Option B", "B");
            radio.AddOption("Option C", "C");

            // Add the field to the document's form collection.
            doc.Form.Add(radio);

            // Save the temporary PDF.
            doc.Save(tempPdfPath);
        }

        // ---------------------------------------------------------------------
        // 2. Use the Form facade to read the option values of the radio group.
        // ---------------------------------------------------------------------
        using (Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form(tempPdfPath))
        {
            // GetButtonOptionValues returns a Dictionary<string,string>
            Dictionary<string, string> dict = form.GetButtonOptionValues(radioFieldName);

            // Convert the Dictionary to a Hashtable as required by the task.
            Hashtable optionValues = new Hashtable();
            foreach (KeyValuePair<string, string> kvp in dict)
            {
                optionValues.Add(kvp.Key, kvp.Value);
            }

            // Example usage: print all option names and their export values.
            Console.WriteLine("Radio button option values:");
            foreach (DictionaryEntry entry in optionValues)
            {
                Console.WriteLine($"{entry.Key} = {entry.Value}");
            }
        }

        // Clean up the temporary file.
        if (File.Exists(tempPdfPath))
        {
            File.Delete(tempPdfPath);
        }
    }
}
