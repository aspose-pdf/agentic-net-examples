using System;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

namespace BatchPdfFormGeneration
{
    class Program
    {
        static void Main()
        {
            // Step 1: Create a simple PDF template with form fields.
            using (Document template = new Document())
            {
                // Add a blank page (1‑based indexing).
                template.Pages.Add();

                // Create a text box field for "Name".
                TextBoxField nameField = new TextBoxField(template.Pages[1], new Rectangle(100, 700, 300, 720));
                nameField.PartialName = "Name";
                template.Form.Add(nameField);

                // Create a checkbox field for "Member".
                CheckboxField memberField = new CheckboxField(template.Pages[1], new Rectangle(100, 650, 120, 670));
                memberField.PartialName = "Member";
                template.Form.Add(memberField);

                // Save the template to disk.
                template.Save("template.pdf");
            }

            // Step 2: Simulate data that would normally come from a database.
            List<Record> records = new List<Record>
            {
                new Record { Name = "Alice", IsMember = true },
                new Record { Name = "Bob", IsMember = false },
                new Record { Name = "Carol", IsMember = true },
                new Record { Name = "Dave", IsMember = false }
            };

            // Step 3: For each record, load the template, fill the fields, and save a new PDF.
            for (int i = 0; i < records.Count; i++)
            {
                Record current = records[i];
                using (Document doc = new Document("template.pdf"))
                {
                    // Fill the "Name" text box.
                    TextBoxField nameField = doc.Form["Name"] as TextBoxField;
                    if (nameField != null)
                    {
                        nameField.Value = current.Name;
                    }

                    // Set the "Member" checkbox state.
                    CheckboxField memberField = doc.Form["Member"] as CheckboxField;
                    if (memberField != null)
                    {
                        memberField.Checked = current.IsMember;
                    }

                    // Save the filled PDF with a unique name.
                    string outputFile = $"filled_{i + 1}.pdf";
                    doc.Save(outputFile);
                }
            }
        }
    }

    // Simple POCO to represent a data record.
    public class Record
    {
        public string Name { get; set; }
        public bool IsMember { get; set; }
    }
}
