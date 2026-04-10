using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // New values to populate the dropdown (list box) field
        string[] newCountries = { "USA", "Canada", "Mexico" };

        // Load the PDF to read existing items of the list box
        using (Document doc = new Document(inputPdf))
        {
            // Retrieve the list box field by its full name
            ListBoxField listBox = doc.Form["CountryList"] as ListBoxField;
            if (listBox == null)
            {
                Console.Error.WriteLine("List box field 'CountryList' not found.");
                return;
            }

            // Collect the names of the existing options so they can be removed
            List<string> existingItems = new List<string>();
            foreach (var opt in listBox.Options)
            {
                // "Option" objects expose the display/value via the Name property
                existingItems.Add(opt.Name);
            }

            // Use FormEditor to modify the form (no destination path in ctor – bind later)
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPdf);

                // Delete each existing item from the list box
                foreach (string item in existingItems)
                {
                    editor.DelListItem("CountryList", item);
                }

                // Add the new items to the list box
                foreach (string item in newCountries)
                {
                    editor.AddListItem("CountryList", item);
                }

                // Persist changes to the output PDF using the overload that accepts a destination path
                editor.Save(outputPdf);
            }
        }

        Console.WriteLine($"Dropdown field 'CountryList' updated and saved to '{outputPdf}'.");
    }
}
