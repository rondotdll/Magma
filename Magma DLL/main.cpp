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

HWND InitConsole(LPCSTR windowTitle) {

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
	HWND _ConsoleHandle = InitConsole("S7 Garbage Collector");

	const char* splash = "[!!!] THIS EXPLOIT HAS BEEN OPTIMIZED BY STUDIO 7 DEVELOPMENT\n[***] Credits: SpeedStarsKiwi, s7davidj, S0L4RE, Fish Sticks\n\n";

	printf(splash, "\n");

	/* Eyestep Stuff */

	printf("[INF] Initializing random garbage...\n");
	EyeStep::open(L"RobloxPlayerBeta.exe");

	printf("[INF] Random technical jargon...\n");
	int print_call = EyeStep::util::nextCall(EyeStep::scanner::scan_xrefs("Video recording stopped")[0], false, false);
	int print_address = base_offset + EyeStep::util::raslr(print_call - 0x400000);

	printf("[INF] Stabalized 1 of 7 mainframes.\n>   {:x}", print_address);

	typedef int(__cdecl* print_func)(int, const char*, ...);
	print_func rbx_print = (print_func)(print_address);

	std::cout << base_offset << std::endl;
	rbx_print(0, "[MAGMA] Neuralink connection handshake successful.");

	uintptr_t data = scheduler().get();
	std::cout << data << std::endl;

	scheduler().print_jobs();

	uintptr_t datamodel = scheduler().get_datamodel();
	std::cout << datamodel << std::endl;

	uintptr_t script_context = scheduler().get_script_context();
	std::cout << script_context << std::endl;

	uintptr_t global_luastate = scheduler().get_global_luastate();
	std::cout << global_luastate << std::endl;

	printf("[INF] Data Signature: ");

	//rbx_print(_INFO, "[MAGMA] Connection Stabalized.");

	return true;
}

