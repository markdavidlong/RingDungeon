@echo off
set taskname=RDSimpleIface.exe

tasklist /FI "IMAGENAME eq %taskname%" 2>NUL | find /I /N "%taskname%">NUL
if "%ERRORLEVEL%"=="0" (
    echo Task found, terminating...
    taskkill /F /IM %taskname%
) else (
    echo Task not found, skipping termination.
)
