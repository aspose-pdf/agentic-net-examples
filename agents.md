---
name: aspose-pdf-examples
description: AI-friendly C# code examples for Aspose.PDF for .NET
language: csharp
framework: net10.0
package: Aspose.PDF 26.3.0
---

# Aspose.PDF for .NET Examples

AI-friendly repository containing validated C# examples for Aspose.PDF for .NET API.

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET.
When working in this repository:
- Each `.cs` file is a **standalone Console Application** — do not create multi-file projects
- All examples must **compile and run** without errors using `dotnet build` and `dotnet run`
- Follow the conventions, boundaries, and anti-patterns documented below exactly
- Use the **Command Reference** section for build/run commands

## Repository Overview

This repository contains **57** verified C# examples demonstrating Aspose.PDF for .NET capabilities.

- Total Examples: 57
- Categories: 1

## Category Details

### Basic Operations
- Examples: 57
- Guide: [agents.md](./basic-operations/agents.md)

## Boundaries

### ✅ Always

These rules are mandatory for every example.

#### Use explicit types — never use `var`
```csharp
// CORRECT
Document document = new Document("input.pdf");
Page page = document.Pages[1];
TextFragmentAbsorber absorber = new TextFragmentAbsorber("search");

// WRONG
// var document = new Document("input.pdf");
// var page = document.Pages[1];
```

#### Use 1-based indexing for Pages, Annotations, EmbeddedFiles
```csharp
// CORRECT — first page is index 1
Page firstPage = document.Pages[1];
Annotation firstAnnotation = page.Annotations[1];
FileSpecification firstFile = document.EmbeddedFiles[1];

// WRONG — index 0 throws IndexOutOfRangeException
// Page page = document.Pages[0];
```

#### Fully qualify ambiguous types (Rectangle, Color, Path, Image, Point, Matrix)
```csharp
// CORRECT
Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 200, 300, 400);
Aspose.Pdf.Drawing.Rectangle drawRect = new Aspose.Pdf.Drawing.Rectangle(50, 50, 200, 100);
Aspose.Pdf.Color pdfColor = Aspose.Pdf.Color.Blue;

// WRONG — ambiguous CS0104
// Rectangle rect = new Rectangle(100, 200, 300, 400);
// Color color = Color.Blue;
```

#### Use `using` blocks for IDisposable objects
```csharp
// CORRECT
using (Document document = new Document("input.pdf"))
{
    // work with document
    document.Save("output.pdf");
}
```

#### Save the document after all modifications
```csharp
Document document = new Document("input.pdf");
// ... make modifications ...
document.Save("output.pdf");
```

### ⚠️ Ask First

Check with a human before doing any of these:
- **Creating multi-file projects** — each example must be a single `.cs` file
- **Using deprecated APIs** — check the Aspose.PDF changelog for the current API surface
- **Adding NuGet packages** beyond `Aspose.PDF` — the `.csproj` template only includes Aspose.PDF
- **Modifying shared infrastructure** — `.csproj` templates, `agents.md` files, CI configs

### 🚫 Never

See the full **Common Mistakes** section below for code-level prohibitions with examples.
- Never use `var` for variable declarations
- Never use 0-based indexing for `Pages`, `Annotations`, or `EmbeddedFiles`
- Never use unqualified type names for `Rectangle`, `Color`, `Path`, `Image`, `Matrix`, `Point`
- Never use `Aspose.Pdf.Saving` namespace (it does not exist)
- Never mix `Aspose.Pdf.LogicalStructure` and `Aspose.Pdf.Structure` namespaces
- Never modify `agents.md` files — they are auto-generated
- Never modify the `.csproj` template — it is generated

## Common Mistakes (Anti-Patterns)

These are verified mistakes that cause build failures. **Never** use the wrong patterns.

### Rotation Enum Uses On Prefix
The Aspose.Pdf.Rotation enum values use the 'on' prefix, NOT 'Rotate'. The correct values are: Rotation.None, Rotation.on90, Rotation.on180, Rotation.on270, Rotation.on360. Using 'Rotate90', 'Rotate18

```csharp
// WRONG
page.Rotate = Rotation.Rotate180; // CS0117
page.Rotate = Rotation.Rotate90;  // CS0117
```

```csharp
// CORRECT
page.Rotate = Rotation.on180;
page.Rotate = Rotation.on90;
page.Rotate = Rotation.on270;
```

### Prefer Fully Qualified Names For Ambiguous Types
When a type name can resolve to multiple namespaces (especially with Aspose.Pdf, Aspose.Pdf.Drawing, System.IO, and System.Drawing), ALWAYS use fully qualified names for ANY type that exists in more t

```csharp
// WRONG
Rectangle rect = new Rectangle(100, 500, 300, 550); // CS0104
Path.GetFileName("input.pdf");                      // CS0104
Image img = Image.FromFile("photo.jpg");             // CS0104
Color c = Color.Blue;                                // CS0104
```

```csharp
// CORRECT
Aspose.Pdf.Rectangle pageRect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
Aspose.Pdf.Drawing.Rectangle shapeRect = new Aspose.Pdf.Drawing.Rectangle(100, 500, 200, 100);
System.IO.Path.GetFileName("input.pdf");
Aspose.Pdf.Image pdfImg = new Aspose.Pdf.Image();
System.Drawing.Image sysImg = System.Drawing.Image.FromFile("photo.jpg");
Aspose.Pdf.Color pdfColor = Aspose.Pdf.Color.Blue;
System.Drawing.Color sysColor = System.Drawing.Color.Blue;
Aspose.Pdf.Point pdfPoint = new Aspose.Pdf.Point(100, 200);
// ...
```

### Annotation Collection One Based Indexing
Aspose.Pdf annotation collections use 1-based indexing, same as Pages. page.Annotations[1] is the first annotation. page.Annotations[0] throws an exception. page.Annotations.Delete(index) also expects

```csharp
// WRONG
page.Annotations[0]; // throws IndexOutOfRangeException
page.Annotations.Delete(page.Annotations.Count - 1); // deletes second-to-last
```

```csharp
// CORRECT
for (int i = 1; i <= page.Annotations.Count; i++)
{
    Annotation ann = page.Annotations[i];
}
page.Annotations.Delete(page.Annotations.Count);
```

### Annotation Title Requires Markup Annotation
The 'Title' property belongs to MarkupAnnotation, NOT the base Annotation class. When iterating page.Annotations or using FindByName(), the returned type is Annotation which does not have Title. You m

```csharp
// WRONG
Annotation ann = page.Annotations[1];
Console.WriteLine(ann.Title); // CS1061
```

```csharp
// CORRECT
Annotation ann = page.Annotations[1];
string title = ann is MarkupAnnotation markup ? markup.Title : "N/A";
Console.WriteLine($"Title={title}, Contents={ann.Contents}");
Annotation retrieved = page.Annotations.FindByName("MyNote");
if (retrieved is MarkupAnnotation ma)
{
    Console.WriteLine(ma.Title);
}
```

### Border Requires Parent Annotation
The Border class has no parameterless constructor — it requires an Annotation parent argument: new Border(annotation). Border also has NO Color property; border color is controlled by the annotation's

```csharp
// WRONG
var ann = new TextAnnotation(page, rect)
{
    Border = new Border { Width = 1, Color = Aspose.Pdf.Color.Black }
};
Errors: CS1729, CS1061
var ann = new TextAnnotation(page, rect)
...
```

```csharp
// CORRECT
TextAnnotation ann = new TextAnnotation(page, rect)
{
    Title = "Note",
    Contents = "Hello",
    Color = Aspose.Pdf.Color.Yellow
};
ann.Border = new Border(ann) { Width = 1 };
```

### Qualify Rectangle And Color
Always use fully qualified Aspose.Pdf.Rectangle and Aspose.Pdf.Color to avoid ambiguity with System.Drawing types. Even if System.Drawing is not explicitly referenced, implicit usings or transitive de

```csharp
// WRONG
Rectangle rect = new Rectangle(100, 500, 300, 550);
txtAnn.Color = Color.Yellow;
```

```csharp
// CORRECT
Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
txtAnn.Color = Aspose.Pdf.Color.Yellow;
```

### Mhtsaveoptions Namespace Missing
The MhtSaveOptions class is not in the Aspose.Pdf namespace — it resides in Aspose.Pdf.Facades. You must add 'using Aspose.Pdf.Facades;' and ensure the Aspose.PDF.dll reference includes the Facades as

```csharp
// WRONG
using Aspose.Pdf; // alone is insufficient
MhtSaveOptions mhtOptions = new MhtSaveOptions(); // ❌ CS0246
```

```csharp
// CORRECT
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Facades; // ← Required for MhtSaveOptions
MhtSaveOptions mhtOptions = new MhtSaveOptions
{
    PartsEmbeddingMode = MhtSaveOptions.PartsEmbeddingModes.EmbedAllIntoMht
};
```

### Caret Annotation Symbol Missing
The enum 'CaretAnnotationSymbol' does not exist in Aspose.Pdf.Annotations. The 'Symbol' property of 'CaretAnnotation' expects a value of type 'Aspose.Pdf.Annotations.CaretSymbol', which is an enum def

```csharp
// WRONG
Symbol = CaretAnnotationSymbol.Insert // ❌ enum does not exist
```

```csharp
// CORRECT
using Aspose.Pdf.Annotations;
...
CaretAnnotation caret = new CaretAnnotation(page, rect)
{
    Symbol = CaretSymbol.Insert,
    Contents = "...",
    Color = Aspose.Pdf.Color.Red
};
```

### Fix Texfragment Margin And Color Ambiguity
The TeXFragment class does not have a 'Margin' property; instead, use 'TopMargin', 'BottomMargin', etc. Also, 'Color' is ambiguous between System.Drawing.Color and Aspose.Pdf.Color — use fully qualifi

```csharp
// WRONG
TeXFragment texFragment = new TeXFragment(...);
texFragment.Margin = new Margin { Top = 20, Bottom = 20 }; // ❌ No such property
DefaultAppearance appearance = new DefaultAppearance(..., Color.Black); // ❌ ambiguous
```

```csharp
// CORRECT
TeXFragment texFragment = new TeXFragment(@"\frac{a}{b} = c", true);
texFragment.HorizontalAlignment = HorizontalAlignment.Center;
texFragment.TopMargin = 20;
texFragment.BottomMargin = 20;
DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, Aspose.Pdf.Color.Black);
```

### Resolve Color Ambiguity In Defaultappearance
When constructing DefaultAppearance, use fully qualified Aspose.Pdf.Color (not System.Drawing.Color) to avoid ambiguity. The constructor expects Aspose.Pdf.Color, and System.Drawing.Color is not impli

```csharp
// WRONG
DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, Color.Black); // ❌ ambiguous
```

```csharp
// CORRECT
DefaultAppearance appearance = new DefaultAppearance("Helvetica", 12, Aspose.Pdf.Color.Black);
```

## Domain Knowledge

Cross-cutting rules and API-specific gotchas.

- **Create a new {doc} and add a {page} via {doc}.Pages.Add().**
  _(Applies to: Graphs, Working-Document)_
- **Set {table}.ColumnWidths to a space‑separated string of column widths (e.g., "40 100 100") and assign {table}.DefaultCellBorder = new BorderInfo(BorderSide.All, {float}) to apply a uniform border to all cells.**
  _(Applies to: Tables)_
- **Populate the table from a System.Data.DataTable using {table}.ImportDataTable({data_table}, true, 0, 0, {int}, {int}) where the last two integers represent the total number of rows (including header) and the number of columns.**
  _(Applies to: Tables)_
- **Iterate over {row}.Cells to customize appearance: set {cell}.BackgroundColor, {cell}.DefaultCellTextState.Font, {cell}.DefaultCellTextState.ForegroundColor, and {cell}.DefaultCellTextState.HorizontalAlignment.**
  _(Applies to: Tables)_
- **Apply distinct styling to the header row (row index 0) versus data rows (row index >= 1) by using separate loops or conditional logic.**
  _(Applies to: Tables)_
- **Create a TextFragmentAbsorber with a {string_literal} search phrase and apply it to a specific {page} of a {doc} via page.Accept(absorber).**
  _(Applies to: Text)_
- **Iterate over absorber.TextFragments; for each {text_fragment}, set Text = {string_literal} and adjust TextState properties: Font = FontRepository.FindFont({font}), FontSize = {float}, ForegroundColor = Color.FromRgb({color}), BackgroundColor = Color.FromRgb({color}).**
  _(Applies to: Text)_

## Command Reference

### Build and Run
```bash
# Create a new project (if needed)
dotnet new console -n ExampleProject --framework net10.0

# Add Aspose.PDF NuGet package
dotnet add package Aspose.PDF --version 26.3.0

# Build
dotnet build --configuration Release --verbosity minimal

# Run
dotnet run
```

### Project File (.csproj)
```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Aspose.PDF" Version="26.3.0" />
  </ItemGroup>
</Project>
```

### Environment
- .NET SDK: 10.0 or higher
- NuGet: Aspose.PDF 26.3.0
- All examples are standalone Console Applications
- Each `.cs` file can be compiled and run independently

## Testing Guide

Every example must pass these verification steps.

### Build Verification
```bash
dotnet build --configuration Release --verbosity minimal
```
- **Success**: Exit code 0, no `CS` error codes in output
- **Failure**: Any `error CS####` line indicates a build failure

### Run Verification
```bash
dotnet run
```
- **Success**: Exit code 0, no unhandled exception stack traces
- **Failure**: `Unhandled exception`, `System.Exception`, or non-zero exit code

### Expected Output Patterns
- Console output confirming the operation (e.g., "PDF saved successfully")
- Output files created in the working directory (e.g., `output.pdf`)
- No `NullReferenceException`, `IndexOutOfRangeException`, or `FileNotFoundException`

### Common Error Codes
| Code | Meaning | Fix |
|------|---------|-----|
| `CS0104` | Ambiguous type reference | Use fully qualified name (`Aspose.Pdf.Rectangle`) |
| `CS1061` | Member does not exist on type | Check API docs for correct property/method |
| `CS0246` | Type or namespace not found | Add missing `using` directive |
| `CS0029` | Cannot convert type | Cast explicitly or use correct type |

## How to Use These Examples

### Prerequisites
- .NET SDK (10.0 or higher)
- Aspose.PDF for .NET (26.3.0 or higher)
- NuGet package restore enabled

### Running an Example
1. Navigate to any category folder
2. Each .cs file is a standalone Console Application
3. Ensure `input.pdf` exists in the same directory (where required)
4. Compile and run:
   ```bash
   dotnet run <example-file.cs>
   ```

<!-- AUTOGENERATED:START -->
Updated: 2026-03-27 | Run: `20260327_101929_00fc82`
<!-- AUTOGENERATED:END -->

---

*This repository is maintained by automated code generation. Last updated: 2026-03-25 | Total examples: 57*
