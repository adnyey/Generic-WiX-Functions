# Generic-WiX-Functions

I had to solve some difficult problems in Wix Installer. Since WiX does not provide all utilities, I created some to do that for me.

QuiteCmd()
This function executes a command line in the background without a pop-up dialog. The output is also captured in the logs. This is a deferred Custom Actions so please treat it as such.

To use this function:
1. Create an Immediate Custom Action which assigns the CustomActionData properties for the custom action which calls this function. 
2. Sequence the immediate custom action before the intended deferred custom action. Done.
