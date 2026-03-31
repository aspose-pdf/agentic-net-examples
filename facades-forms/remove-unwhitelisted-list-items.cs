using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName = "State";

        // Predefined whitelist of allowed items
        string[] whitelist = new string[] { "Approved", "Pending", "Rejected" };
        // Example list of all items currently present in the list field
        string[] allItems = new string[] { "Approved", "Pending", "Rejected", "Draft", "Obsolete" };

        // Verify that the source PDF exists to avoid runtime FileNotFoundException
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file '{inputPath}' not found. Operation aborted.");
            return;
        }

        // Load the PDF document first – FormEditor now expects a Document instance.
        using (Document pdfDoc = new Document(inputPath))
        using (FormEditor formEditor = new FormEditor(pdfDoc))
        {
            foreach (string item in allItems)
            {
                bool isWhitelisted = false;
                foreach (string allowed in whitelist)
                {
                    if (item.Equals(allowed, StringComparison.OrdinalIgnoreCase))
                    {
                        isWhitelisted = true;
                        break;
                    }
                }
                if (!isWhitelisted)
                {
                    // Delete the item from the list field
                    formEditor.DelListItem(fieldName, item);
                }
            }

            // Save the modified PDF to the desired output path
            formEditor.Save(outputPath);
        }

        Console.WriteLine("Unwhitelisted items removed. Saved to " + outputPath);
    }
}
