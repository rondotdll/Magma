#pragma once
#pragma warning

#include <cstdint>
#include <vector>
#include <string>
#include <iostream>
#include <libloaderapi.h>

#include "procutil.h"
#include "sigscan.h"

// Stores the first address of the Roblox* process
/* in actuality its the first address of whatever application the DLL is injected into */
uintptr_t base_offset = reinterpret_cast<uintptr_t>(GetModuleHandleA(NULL));


/*  >>>>  THESE TWO ADDRESSES CHANGE FREQUENTLY  <<<<  */

// Address of Roblox's Task Scheduler class?
/* https://create.roblox.com/docs/optimization/microprofiler/task-scheduler */
uintptr_t scheduler_address = base_offset + 0x6D9250;


uintptr_t getstate_address = base_offset + 0x367380;

uintptr_t jobs_start(0x134);
uintptr_t jobs_end(0x138);
uintptr_t job_name1(0x10);
uintptr_t datamodel_job(0x28);
uintptr_t script_context_job(0x130);

ProcUtil::ModuleInfo main_module{};
HANDLE handle = NULL;