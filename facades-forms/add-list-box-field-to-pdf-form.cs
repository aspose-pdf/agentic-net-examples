using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms; // needed for FieldType and ListBox handling

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_priority.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Create the list box field using FormEditor (real API members only)
            // -----------------------------------------------------------------
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(doc);

                // Define the rectangle where the list field will appear
                Rectangle rect = new Rectangle(100, 600, 250, 650);

                // Add an empty ListBox field (FieldType.ListBox) on page 1
                // AddField expects float parameters for the coordinates, so cast the double values
                editor.AddField(FieldType.ListBox, "Priority", 1,
                                (float)rect.LLX, (float)rect.LLY,
                                (float)rect.URX, (float)rect.URY);

                // Populate the list with the required items
                editor.AddListItem("Priority", "Low");
                editor.AddListItem("Priority", "Medium");
                editor.AddListItem("Priority", "High");
            }

            // ---------------------------------------------------------------
            // 2. Set the default selected item (value) using the Form facade
            // ---------------------------------------------------------------
            // Use the Form class from Aspose.Pdf.Facades to avoid ambiguity with Aspose.Pdf.Forms.Form
            using (Aspose.Pdf.Facades.Form formFacade = new Aspose.Pdf.Facades.Form())
            {
                formFacade.BindPdf(doc);
                // For a ListBox the value to fill is the item text you want selected
                formFacade.FillField("Priority", "Medium");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with list field saved to '{outputPath}'.");
    }
}
