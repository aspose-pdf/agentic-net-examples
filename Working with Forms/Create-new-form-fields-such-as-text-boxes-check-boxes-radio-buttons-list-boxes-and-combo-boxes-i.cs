using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths
        const string outputPdf = "FormFields.pdf";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            // Add a blank page (page numbers are 1‑based)
            doc.Pages.Add();

            // Initialize FormEditor on the created document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // -------------------------------------------------
                // 1. Text Box
                // -------------------------------------------------
                // FieldType.Text creates a single‑line text box.
                // Rectangle coordinates: llx, lly, urx, ury (points)
                formEditor.AddField(FieldType.Text, "TextBox1", 1,
                                    100f, 700f,   // lower‑left
                                    250f, 730f); // upper‑right

                // -------------------------------------------------
                // 2. Check Box
                // -------------------------------------------------
                formEditor.AddField(FieldType.CheckBox, "CheckBox1", 1,
                                    100f, 650f,   // lower‑left
                                    120f, 670f); // upper‑right

                // -------------------------------------------------
                // 3. Radio Button Group
                // -------------------------------------------------
                // Define the options that will be rendered as individual radio buttons.
                formEditor.Items = new string[] { "Red", "Green", "Blue" };
                // Optional: change layout (horizontal = true, vertical = false)
                formEditor.RadioHoriz = false; // arrange vertically
                // Add the radio button group field. The size defines the area that will contain the options.
                formEditor.AddField(FieldType.Radio, "ColorChoice", 1,
                                    100f, 500f,   // lower‑left
                                    120f, 560f); // upper‑right

                // -------------------------------------------------
                // 4. List Box
                // -------------------------------------------------
                formEditor.AddField(FieldType.ListBox, "ListBox1", 1,
                                    300f, 700f,   // lower‑left
                                    450f, 500f); // upper‑right
                // Populate the list box with items
                formEditor.AddListItem("ListBox1", "Apple");
                formEditor.AddListItem("ListBox1", "Banana");
                formEditor.AddListItem("ListBox1", "Cherry");

                // -------------------------------------------------
                // 5. Combo Box
                // -------------------------------------------------
                formEditor.AddField(FieldType.ComboBox, "ComboBox1", 1,
                                    300f, 600f,   // lower‑left
                                    450f, 630f); // upper‑right
                // Populate the combo box with items
                formEditor.AddListItem("ComboBox1", "Option A");
                formEditor.AddListItem("ComboBox1", "Option B");
                formEditor.AddListItem("ComboBox1", "Option C");

                // Save the modified PDF to the specified file
                formEditor.Save(outputPdf);
            }
        }

        Console.WriteLine($"PDF with form fields created: {outputPdf}");
    }
}