using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        using (MemoryStream pdfStream = new MemoryStream())
        {
            // ---------------------------------------------------------------------
            // 1. Create a sample PDF in memory that contains a radio button group.
            // ---------------------------------------------------------------------
            Document doc = new Document();
            Page page = doc.Pages.Add();

            // Define the rectangle where the radio button field will be placed.
            // (left, bottom, right, top) – values are in points.
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 700, 150, 720);

            // Create the radio button field.
            // Use the constructor that accepts only a Page object, then set the field name via PartialName.
            RadioButtonField radio = new RadioButtonField(page);
            radio.PartialName = "RadioGroup"; // field name
            radio.Rect = rect; // assign the rectangle to the field

            // Add options (export value, display value).
            radio.AddOption("opt1", "Option 1");
            radio.AddOption("opt2", "Option 2");
            radio.AddOption("opt3", "Option 3");

            // Add the field to the document's form collection.
            doc.Form.Add(radio);

            // Save the PDF to the memory stream.
            doc.Save(pdfStream);
            pdfStream.Position = 0; // rewind for reading

            // ---------------------------------------------------------------
            // 2. Use the Form facade to read the radio button options.
            // ---------------------------------------------------------------
            Aspose.Pdf.Facades.Form form = new Aspose.Pdf.Facades.Form();
            form.BindPdf(pdfStream); // bind the in‑memory PDF

            // Retrieve the option values for the radio button group.
            // The method returns a Dictionary<string, string> where:
            //   Key   = export name (option value)
            //   Value = display name (or export value, depending on the PDF)
            Dictionary<string, string> optionDict = form.GetButtonOptionValues("RadioGroup");

            // Transfer the dictionary entries into a Hashtable as required.
            Hashtable optionTable = new Hashtable();
            foreach (KeyValuePair<string, string> kvp in optionDict)
            {
                optionTable.Add(kvp.Key, kvp.Value);
            }

            // Example usage: print all options stored in the Hashtable
            Console.WriteLine("Radio button options retrieved from the PDF:");
            foreach (DictionaryEntry entry in optionTable)
            {
                Console.WriteLine($"Option Name: {entry.Key}, Value: {entry.Value}");
            }

            // Clean up the Form facade
            form.Close();
        }
    }
}
