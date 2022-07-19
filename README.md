# **HCI Online Enrollment System**

## *As of June 14, 2022* 
[VB.net]
    
- Working Email Generation.
- Working OVRF Generation.
- Working Accounting.
- Working Enrollment.
- No file upload function.


[Website]
- Added initial commit with index.html and register.html (no functionality, just display).

## *As of June 22, 2022*

[Website]
- Added a generates function and can now upload files and information to database.

## *As of June 23, 2022*

[Website]
- Clean the php code and centered the divs in index.html.
- Website is now responsive to wrong user input in file upload.
- PHP is now checking if you have already enrolled or not.

## *As of June 28, 2022*

[Website, VB.net]
- Year and semester are now also included when enrolling.
- As a 2nd-4th year, you have to give your existing Student NO.
- Transferees are now able to choose year and course.
- Added a safeguard check for user-inputs by using trim() fucntion to remove whitespaces.
- Can now select a gender for Freshmen and Transferee.
- Can now remove directories that already exist.

[VB.net]
- File upload are now responsive.

## *As of July 13, 2022*

[VB.net]
- Added a flat design for onlineEnrollment.
- Will do the same for other forms later.

## *As of July 14, 2022*

[VB.net]
- All of the forms in VB.net have now flat design.
- I did however bring back the round edges. It is just not that aesthetic.

## *As of July 16, 2022*

[Website]
- Fixed a problem on onlineEnrollment where students name are not registring in "enrolled.txt".

## *As of July 19, 2022*

[VB.net]
- Added a shadow to the form so it will be differentiable to white backgrounds.
- Added comments only to all VB.net programs.
- Added *ENROLL ALL* and *REGISTER* function to Registration Form.
- OVRFs default file are now segregated for each year, each course, and each semester.
- OVRFs for students are now also segregated.
- Fixed the wrong file paths on generateOVRF() function.
- Replaced SQL commands where it is still "classes" where it should be {n}_year.
- *REGISTER* button is now only putting 'checked' tag on the student (previously, it is also putting a section to the student).
- Enrolling as 2nd-4th Year now also remove the 'checked' tag to the student.

[Website]
- There is now a safeguard when creating a folder when enrolling as 2nd-4th year.
- Fixed a problem on onlineEnrollment where students with "None" middlename is registring "None" middlename in database.
- Website has now an icon on its browser tab.