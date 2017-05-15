# NintenTools.MarioKart8

The goal of this .NET library is to provide managed access to data stored in Mario Kart 8 (Deluxe) specific file formats. A collection of tools, based on the library, is offered aswell.

The library is available as a [NuGet package](https://www.nuget.org/packages/Syroot.NintenTools.MarioKart8).

Right now, the following file formats and files are supported:

| Description             | File Name(s)     | Load   | Save | Notes                                                                |
|-------------------------|:----------------:|:------:|:----:|----------------------------------------------------------------------|
| Item Probability Tables | Item.bin         | Yes    | Yes  | Strongly typed, tool exists (Item Editor)                            |
| Physics & Engine Stats  | Performance.bin  | Yes    | Yes  | Strongly typed, tool exists (Performance Editor)                     |
| Other BIN Files         | \*.bin           | Yes    | Yes  |                                                                      |
| Obj Definition Database | objflow.byaml    | Yes    | Yes  | Strongly typed via `Courses.Objs.ObjDefinitionDb`                    |
| Course Definitions      | \*_muunt\*.byaml | Yes    | Yes  | Strongly typed via `Courses.CourseDefinition`                        |
| Other BYAML Files       | \*.byaml         | Yes    | Yes  | Use [NintenTools.Byaml](https://github.com/Syroot/NintenTools.Byaml) |
| Visual Models           | *.bfres          | Partly | No   | Use [NintenTools.Bfres](https://github.com/Syroot/NintenTools.Bfres) |
| KCL Collision Models    | course.kcl       | Yes    | No   |                                                                      |
| Yaz0 Compressed Data    | \*.szs / others  | Yes    | No   | Use [NintenTools.Yaz0](https://github.com/Syroot/NintenTools.Yazo)   |

Tools
=====

The repository contains the following tools. Please note these were developed for testing purposes and do not receive support.
- **Item Editor**: Shows item probability distributions and distances of an **Item.bin** file and allows editing and saving new versions.
![Item Editor](https://raw.githubusercontent.com/Syroot/NintenTools.MarioKart8/master/doc/readme/item_editor.png)
- **Performance Editor**: Shows physics and point set details of a **Performance.bin** file and allows editing and saving new versions.
![Performance Editor](https://raw.githubusercontent.com/Syroot/NintenTools.MarioKart8/master/doc/readme/performance_editor.png)
- **Adjust200**: Takes a **course_muunt.byaml** and adds `Adjuster200cc` Objs into it from a **course_muunt_200.byaml** file in the same directory, overwriting the latter to create a new 200cc BYAML file.
- **BinDumper**: Dumps the data of **&ast;.bin** files as found in their section, group and element hierarchy.
- **NoLakitu**: Removes `EnemyPath` and `LapPath` from a **&ast;_muunt&ast;.byaml** (and Objs having referenced those) in order to get rid of Lakitu who would prevent you from going out of bounds.
- **ObjDumper**: Dumps the information found in **objflow.byaml** into a readable table.
