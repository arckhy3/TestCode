CREATE TABLE tr_bpkb (
    agreement_number varchar(100) PRIMARY KEY,
    bpkb_no varchar(100),
    branch_id varchar(10),
    bpkb_date datetime,
    faktur_no varchar(100),
    faktur_date datetime,
    location_id varchar(10),
    police_no varchar(20),
    bpkb_date_in datetime,
    created_by varchar(20),
    created_on datetime,
    last_updated_by varchar(20),
    last_updated_on datetime,
    CONSTRAINT FK_tr_bpkb_location_id FOREIGN KEY (location_id) 
        REFERENCES ms_storage_location(location_id)
);