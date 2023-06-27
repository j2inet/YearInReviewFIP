# Year In Progress for Logitech/Saitech Flight Instrument Panels

I recently acquired a Logitech/Saitech flight instrument panel and wanted to see if I could change the content that displays
on the device when it is idle. I don't particularly like for my devices to show my ads when they are not in use. As it turns
out, the device is shows JPG files from a specific folder when it is idle. You can overwrite any of the images and they will 
automatically be detected and displayed on the device. 

The service that controls the screen reads the JPGs from `c:\Program Files\Logitech\DirectOutput.` As long as the file is a JPG, 
it will be detected and displayed. Note, however, that only administrators have access to modify files in this folder. 

I want to display something that is somewhat useful or interesting in place of the advertisements. Something that would be quick 
and simple to put together is a simple progress bar to show how much of my day has passed by. This code repository is the application
for doing this. 