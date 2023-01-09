#pragma once

#define _CRT_SECURE_NO_WARNINGS
#pragma warning(disable: 4996)

#include <Windows.h>
#include <cstdint>
#include <vector>
#include <string>
#include <iostream> 

#include "eyestep.h"
#include "addresses.h"
#include "scheduler.h"
#include "eyestep_utility.h"

const int _INFO = 0;
const int _WARN = 1;
const int _ERROR = 2;

HWND StartConsole(LPCSTR windowTitle) {

	DWORD PAGE_WRITE;
	VirtualProtect((PVOID)&FreeConsole, 1, PAGE_EXECUTE_READWRITE, &PAGE_WRITE); // Allows us to access console-related functions (..?)

	*(uint8_t*)(&FreeConsole) = 0xC3; // ???
	AllocConsole();
	
	SetConsoleTitleA(windowTitle);
	freopen("CONOUT$", "w", stdout);
	freopen("CONIN", "r", stdin);

	HWND _Handle = GetConsoleWindow();
	::SetWindowPos(_Handle, HWND_TOPMOST, NULL, NULL, NULL, NULL, SWP_DRAWFRAME | SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);
	::ShowWindow(_Handle, SW_SHOW);

	return _Handle;
}

auto __stdcall DllMain(void*, std::uint32_t call_reason, void*) -> bool 
{
	HWND _ConsoleHandle = StartConsole("S7 Garbage Collector");

	const char* splash = R"ms(
[!!!] THIS EXPLOIT HAS BEEN OPTIMIZED BY STUDIO 7 DEVELOPMENT
[***] Credits: SpeedStarsKiwi, s7davidj, S0L4RE, Fish Sticks

)ms";

	printf(splash, "\n");

	/* Eyestep Stuff */

	printf("[INF] Initializing random garbage...\n");
	EyeStep::open(L"RobloxPlayerBeta.exe");

	printf("[INF] Random technical jargon...\n");
	int print_call = EyeStep::util::nextCall(EyeStep::scanner::scan_xrefs("Video recording stopped")[0], false, false);
	int print_address = base + EyeStep::util::raslr(print_call - 0x400000);
	
	printf("[INF] Stabalized 1 of 7 mainframes.\n", ">   ", print_address);

	typedef int(__cdecl* print_func)(int, const char*, ...);
	print_func rbx_print = (print_func)(print_address);

	rbx_print(0, "[MAGMA] Neuralink connection handshake successful.");
	//rbx_print(_INFO, "[MAGMA] Stabalizing connection... (1/5)");
	uintptr_t data = scanner().get();
	printf("[INF] Stabalized 2 of 7 mainframes...\n", ">   ");
	std::cout << data << std::endl;

	//rbx_print(_INFO, "[MAGMA] Stabalizing connection... (2/5)");
	scanner().print_jobs();
	printf("[INF] Stabalized 3 of 7 mainframes...\n", ">   ", "N/A");
	
	//rbx_print(_INFO, "[MAGMA] Stabalizing connection... (3/5)");
	uintptr_t datamodel = scanner().get_datamodel();
	printf("[INF] Stabalized 4 of 7 mainframes...\n", ">   ");
	std::cout << datamodel<< std::endl;

	//rbx_print(_INFO, "[MAGMA] Stabalizing connection... (4/5)");
	uintptr_t script_context = scanner().get_script_context();
	printf("[INF] Stabalized 5 of 7 mainframes...\n", ">   ");
	std::cout << script_context<< std::endl;

	//rbx_print(_INFO, "[MAGMA] Stabalizing connection... (5/5)");
	uintptr_t global_luastate = scanner().get_global_luastate();
	printf("[INF] Stabalized 6 of 7 mainframes...\n", ">   ");
	std::cout << global_luastate << std::endl;

	printf("[INF] Data Signature: ", scanner().get_global_luastate());

	//rbx_print(_INFO, "[MAGMA] Connection Stabalized.");

	return true;
}

