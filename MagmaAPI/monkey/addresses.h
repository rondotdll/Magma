#pragma once
#pragma warning

#include <cstdint>
#include <vector>
#include <string>
#include <iostream>
#include <libloaderapi.h>

uintptr_t base = reinterpret_cast<uintptr_t>(GetModuleHandleA(NULL));

//// Address for Roblox's built-in print-to-console function
//const auto extern print_address;
//#define print_convention __cdecl
//
//// Address for unknown data
//const auto extern type_names_address = NULL;
//#define type_names_convention __cdecl;
//
//// Address for unknown data
//const auto extern task_scheduler = NULL;
//#define task_scheduler_convention __cdecl
//
//// Address for unknown data
//const auto extern new_thread_address = NULL;
//#define new_thread_convention __cdecl
//
//// Address for the LuaU Deserialization function
//const auto extern deserialize_address = NULL;
//#define deserialize_convention __cdecl;
//
//// Address for unknown data
//const auto extern lua_state_address = NULL;