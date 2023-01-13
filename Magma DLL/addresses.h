#pragma once
#pragma warning

#include <cstdint>
#include <vector>
#include <string>
#include <iostream>
#include <Windows.h>

#include "procutil.h"
#include "sigscan.h"

/* Some friendly documentation from your pals @ S7 Development
 *
 * Memory Addresses... oh boy.
 *
 * Memory addresses are another crucial part of any exploit, as
 * they represent the parts of the Roblox client we need to send
 * our data to. When the Roblox process is open, it is being
 * stored in system memory (aka RAM), so in order to execute our
 * scripts we need to know which part of memory Roblox is in.
 *
 * Throughout this file are a few more comments explaining what
 * each address is used for
 *
 * For more information on Memory Addresses, click the link below:
 * https://www.techopedia.com/definition/323/memory-address
 */

 // Stores the first address of the Roblox* process
 /* in actuality its the first address of whatever application the DLL is injected into */

 /*  >>>>  THESE TWO ADDRESSES CHANGE FREQUENTLY  <<<<  */

 // Address of Roblox's Task Scheduler class?
 /* https://create.roblox.com/docs/optimization/microprofiler/task-scheduler */

 //uintptr_t scheduler_address = base_offset + 0x6D9250;



 //uintptr_t getstate_address = base_offset + 0x367380;

struct AddressContext {
    uintptr_t base_offset;
    uintptr_t scheduler_address;
    uintptr_t getstate_address;
    ProcUtil::ModuleInfo main_module;
    HANDLE handle;
};

// Offsets...
const uintptr_t jobs_start(0x134);
const uintptr_t jobs_end(0x138);
const uintptr_t job_name1(0x10);
const uintptr_t datamodel_job(0x28);
const uintptr_t script_context_job(0x130);


AddressContext create_address_context();
