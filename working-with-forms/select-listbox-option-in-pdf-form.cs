using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF with a ListBox field
        const string outputPath = "output.pdf";         // PDF after selection is set
        const string listBoxName = "MyListBox";         // full name of the ListBox field
        const int desiredIndex = 2;                     // index of the option to select (1‑based)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            ListBoxField listBox = null;

            // Locate the ListBox field by its full name
            foreach (Page page in doc.Pages)
            {
                foreach (Annotation ann in page.Annotations)
                {
                    if (ann is ListBoxField lb && lb.FullName == listBoxName)
                    {
                        listBox = lb;
                        break;
                    }
                }
                if (listBox != null) break;
            }

            if (listBox == null)
            {
                Console.Error.WriteLine($"ListBox field '{listBoxName}' not found.");
            }
            else
            {
                // Set the selected option (items are numbered from 1)
                listBox.Selected = desiredIndex;
                Console.WriteLine($"ListBox '{listBoxName}' set to option index {desiredIndex}.");
            }

            // Save the modified PDF (lifecycle rule: Save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}