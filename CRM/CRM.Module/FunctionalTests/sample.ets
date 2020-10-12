;#DropDB CRMEasyTest

#Application CRMWin
#Application CRMWeb

*FillForm
 User Name = Admin
*Action Log In

#IfDef CRMWin

*Action About...
 
*Action OK

#EndIf

#IfDef CRMWeb
*Action Navigation(My Details)
#EndIf
